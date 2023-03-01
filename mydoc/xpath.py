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
attr := ( Name | Int | '' | '*' ) [ '[' cond {',' cond}  ']' ]
cond := keys=Value {'&' keys=Value }
keys := Name {'.' Name}

'''
import json
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
            
class Attr:
    def Parse(self,lex:Lexer):
        tk = lex.LookHead()
        self.name_or_idx = ''
        self.conds:list[Cond] = []
        if tk in ['.',',']:
            return
        if tk != '[':
            self.name_or_idx = lex.NextToken()
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


def search(path:str,data):
    print("")

def _test_parse(path:str):
    lex = Lexer(path)
    root = RootPath()
    root.Parse(lex)
    print("ok")

if __name__ == "__main__":
    print('test xpath')
    _test_parse("x.(y[m=hahaha&n=null],z).xx")
    _test_parse("a.(b.c,0.1.2).d | x.(y[m=hahaha&n=null],z).xx")