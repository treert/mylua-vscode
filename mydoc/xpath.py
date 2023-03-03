'''
Path example:
- a.b.c
- a.1.c
- a..c
- a[x=1 & y=2 , z=3 ].(p1,p2)
- a.*.c | 1

Syntax 0.1 very simple: 不支持转义，限制很多，不过够用

rootpath := path { '|' path}
path := seg {'.' seg} 
seg := attr | '(' path {',' path} ')'
attr := ( Name | Int | '' | '*' | '**' ) [ '[' cond {',' cond}  ']' ]
cond := keys=Value {'&' keys=Value }
keys := Name {'.' Name}

'''
import json
import types
from typing import Any

class Lexer:
    Dummy = object()
    EOF = object() # 标记结束
    def __init__(self, path:str) -> None:
        self.path = path
        self.leftpath = path # py slice string
        self.last_tk = Lexer.Dummy
        self._head = Lexer.Dummy
        pass
    
    def LookHead(self):
        if self._head == Lexer.Dummy:
            self._head = self._GetNextToken()
        return self._head
    
    def NextToken(self,expect_json_value:bool=False):
        if self._head != Lexer.Dummy:
            ret = self._head
            self._head = Lexer.Dummy
        else:
            ret = self._GetNextToken()
        if ret == Lexer.EOF:
            return Lexer.EOF
        if expect_json_value and type(ret) == type(''):
            try:
                ret = json.loads(ret)
            except:
                pass # 保持字符串原样
        return ret

    def Raise(self, msg):
        tk = self.last_tk == Lexer.EOF and "<EOF>" or self.last_tk
        raise Exception(f"path format error at {len(self.path) - len(self.leftpath)} last_tk={tk} {msg or ''}")
    
    def _GetNextToken(self, expect_json_value:bool=False):
        '''
        读取新的token
        '''
        cur = self.leftpath.lstrip()
        self.last_tk = Lexer.EOF
        if len(cur) > 0:
            tk = cur[0]
            sp_tks = '.*[]()|&,='
            if sp_tks.find(tk) >= 0:
                self.last_tk = tk
                cur = cur[1:]
            else:
                idx = 1
                while idx < len(cur) and sp_tks.find(cur[idx]) < 0:
                    idx += 1
                tk = cur[:idx].strip()
                try:
                    tk = int(tk)
                except:
                    pass # 保留原始字符串，这儿实际应该校验一下的，不过不管了
                self.last_tk = tk
                cur = cur[idx:]

        self.leftpath = cur
        return self.last_tk

def _try_get_item_by_key(key,data):
    try:
        ret = data.__getitem__(key)
        return (ret,True)
    except:
        return (None,False)

def _get_item_by_keys(keys:list, data):
    try:
        for key in keys:
            data = data.__getitem__(key)
    except:
        return None
    return data

class Cond:
    def Parse(self, lex:Lexer):
        self.kvs:list[(list,Any)] = []
        while lex.LookHead() != Lexer.EOF:
            if lex.LookHead() in [',',']']:
                return
            k = [lex.NextToken()]
            v = None
            while (tk:= lex.NextToken()) == '.':
                k.append(lex.NextToken())
            if tk != '=':
                lex.Raise("expect = for cond")
            v = lex.NextToken(True)
            self.kvs.append((k,v))

            if lex.LookHead() == '&':
                lex.NextToken()
                continue

            if lex.LookHead() not in [',',']']:
                lex.Raise("expect , or ] to end cond")
            
    def Check(self, data:Any):
        for (keys,value) in self.kvs:
            if _get_item_by_keys(keys, data) != value:
                return False
        return True
        
class Attr:
    def Parse(self,lex:Lexer):
        tk = lex.LookHead()
        self.name_or_idx = ''
        self.conds:list[Cond] = []
        if tk in ['.',',']:
            return
        if tk != '[':
            self.name_or_idx = lex.NextToken()
            if self.name_or_idx == '*':
                if lex.LookHead() == '*':
                    lex.NextToken()
                    self.name_or_idx = '**'

        tk = lex.LookHead()
        if tk == '[':
            # condition
            lex.NextToken()
            while lex.LookHead() not in [Lexer.EOF, ']']:
                cond = Cond()
                cond.Parse(lex)
                self.conds.append(cond)
                if lex.LookHead() == ',':
                    lex.NextToken()
                    continue
                else:
                    break
            if lex.LookHead() != ']':
                lex.Raise("expect ] to end cond")
            lex.NextToken()

    def Search(self,data:Any)->list:
        result = []
        if self.name_or_idx == "":
            self._Filter(data, result)
        elif self.name_or_idx == '*':
            Attr._For(data, lambda v: self._Filter(v, result))
        elif self.name_or_idx == '**':
            Attr._ForAll(data, lambda v: self._Filter(v, result))
        else:
            v,ok = _try_get_item_by_key(self.name_or_idx, data)
            if ok:
                self._Filter(v,result)
        return result
        
    def _For(data:Any,call):
        if type(data) == type([]):
            for v in data:
                call(v)
        elif type(data) == type({}):
            for v in data.values():
                call(v)
        pass
    def _ForAll(data:Any,call, level:int = 0):
        level +=1
        if level > 99999:
            raise Exception("data maybe has circle")
        call(data)
        if type(data) == type([]):
            for v in data:
                Attr._ForAll(v,call,level)
        elif type(data) == type({}):
            for v in data.values():
                Attr._ForAll(v,call,level)
        pass

    def _Filter(self,data:Any, result:list):
        if len(self.conds) == 0:
            result.append(data)
            return
        ok = False
        for cond in self.conds:
            if cond.Check(data):
                ok = True
                break
        if ok:
            result.append(data)

class Segment:
    def Parse(self, lex:Lexer):
        self.paths:list[Path] = []
        self.attr:Attr = None
        if lex.LookHead() == '(':
            lex.NextToken()
            while lex.LookHead() not in [Lexer.EOF]:
                path = Path()
                path.Parse(lex)
                self.paths.append(path)
                if (lex.LookHead() == ','):
                    lex.NextToken()
                    continue
                if lex.LookHead() == ')':
                    break
                lex.Raise('expect , or ) after seg-path')
            if lex.NextToken() != ')':
                lex.Raise('expect ) to end seg')
        else:
            attr = Attr()
            attr.Parse(lex)
            self.attr = attr
        pass

    def Search(self,data):
        if self.attr:
            ret = self.attr.Search(data)
            return ret
        else:
            ret = []
            for path in self.paths:
                t = path.Search(data)
                ret += t
            return ret

class Path:
    def Parse(self, lex:Lexer):
        self.segs:list[Segment] = []
        while lex.LookHead() not in [lex.EOF, '|',',',')']:
            seg = Segment()
            seg.Parse(lex)
            self.segs.append(seg)
            if lex.LookHead() == '.':
                lex.NextToken()
                continue
            else:
                break
        if lex.LookHead() not in [lex.EOF, '|',',',')']:
            lex.Raise('expect | , ) to after path')
        pass

    def Search(self,data):
        ret = [data]
        for seg in self.segs:
            t = []
            for d in ret:
                tt = seg.Search(d)
                t += tt
            ret = t
        return ret


class RootPath:
    def Parse(self, lex:Lexer):
        self.paths:list[Path] = []
        while lex.LookHead() != lex.EOF:
            path = Path()
            path.Parse(lex)
            self.paths.append(path)
            if lex.LookHead() == '|':
                lex.NextToken()
                continue
            else:
                break
        if lex.LookHead() != Lexer.EOF:
            lex.Raise("expect | to split path")

    def Search(self, data:Any)->list:
        ret = [data]
        for p in self.paths:
            t = []
            for d in ret:
                tt = p.Search(d)
                t += tt
            ret = t
        return ret

def parse(path:str):
    lex = Lexer(path)
    root = RootPath()
    root.Parse(lex)
    return root

class XPathDataWrap:
    def __init__(self, *data) -> None:
        self.datas:list = [*data]
        pass

    # def __init__(self):
    #     pass

    def search(self, path:str):
        root = parse(path)
        ret = []
        for d in self.datas:
            ret += root.Search(d)
        wrap = XPathDataWrap()
        wrap.datas = ret
        return wrap

def search(path:str,*data)->list:
    wrap = XPathDataWrap(*data)
    ret = wrap.search(path)
    return ret.datas



if __name__ == "__main__":
    import colorama as cc
    from colorama import Style
    def _test_parse(path:str):
        root = parse(path)
        print(f"{cc.Fore.GREEN}parse {path} ok{Style.RESET_ALL}")

    def _test_search(data, *paths):
        print(f'{cc.Fore.BLUE}search in {data}{cc.Style.RESET_ALL}')
        for path in paths:
            print("{: >16} => {}".format(path, search(path,data)))
        
    print('test xpath')
    _test_parse("x.(y[m=hahaha&n=null],z).xx")
    _test_parse("a.(b.c,0.1.2).d | x.(y[m=hahaha&n=null],z).xx")
    print('test xpath.search')
    _test_search([1,2],"")
    _test_search({'x':{'y':{'z':'zzz'}}},"x.y.z")
    _test_search({'a':{'z':1}, 'b':{'z':2}},"[z=1]")
    _test_search({'x':{'m':{'z':1},'n':{'z':2}}}
                 ,"x.(m,n)"
                 ,"x.(m,n).[z=1]"
                 ,"x.(m,n)|[z=1]"
                 ,"x.*.[z=1]"
                 ,"**.[z=1]")
