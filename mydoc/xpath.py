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
seg := attr | '(' attr {',' attr} ')'
attr := ( Name | Int | '' | '*' ) [ '[' cond {',' cond}  ']' ]
cond := keys=Value {'&' keys=Value }
keys := Name {'.' Name}

'''
import json
from typing import Any

class Lexer:
    NotStart = object()
    EOF = object() # 标记结束
    def __init__(self, path:str) -> None:
        self.path = path
        self.leftpath = path # py slice string
        self.cur_tk = Lexer.NotStart 
        pass

    def Raise(self, msg):
        tk = self.cur_tk == Lexer.EOF and "<EOF>" or self.cur_tk
        raise Exception(f"path format error at {len(self.path) - len(self.leftpath)} tk={tk} {msg or ''}")

    def GetCurTK(self):
        return self.cur_tk
    def GetNextToken(self, expect_json_value:bool):
        '''
        读取新的token
        '''
        cur = self.leftpath.lstrip()
        self.cur_tk = Lexer.EOF
        if len(cur) > 0:
            tk = cur[0]
            sp_tks = '.*[]()|&,'
            if sp_tks.find(tk) >= 0:
                self.cur_tk = tk
                cur = cur[1:]
            else:
                idx = 1
                while idx < len(cur) and sp_tks.find(cur[idx]) >= 0:
                    idx += 1
                tk = cur[:idx].strip()
                if expect_json_value:
                    try:
                        tk = json.loads(tk)
                    except:
                        pass # 保留原始字符串
                else:
                    try:
                        tk = int(tk)
                    except:
                        pass # 保留原始字符串，这儿实际应该校验一下的，不过不管了
                self.cur_tk = tk
                cur = cur[idx:]

        self.leftpath = cur
        return self.cur_tk


class Cond:
    def Parse(self, lex:Lexer):
        self.kvs:list[(list,Any)] = []
        while tk:= lex.GetNextToken() != Lexer.EOF:
            if tk in [',',']']:
                break
            k = [tk]
            while tk:= lex.GetNextToken() == '.':
                k.append(lex.GetNextToken())
            if tk not in [',',']']:
                lex.Raise("expect , or ] to end cond")


class Attr:
    def Parse(self,lex:Lexer):
        self.conds:list[list[tuple(str,str)]] = []
        cond_s_idx = seg.find('[')
        if cond_s_idx >= 0:
            # 有条件
            name_e_idx = cond_s_idx
            all_e_idx = seg.index(']')
            cond_str = seg[cond_s_idx+1: all_e_idx-cond_s_idx-2]
            for cond in cond_str.split(','):
                cond_and:list[tuple(str,str)] = []
                for kv in cond.split('&'):
                    (k,v) = kv.split('=')
                    cond_and.append((k,v))
                self.conds.append(cond_and)
        else:
            name_e_idx = seg.find(',')
            if name_e_idx < 0:
                name_e_idx = len(seg)
            all_e_idx = name_e_idx
        name = seg[:name_e_idx].strip() # name|number|''|'*'
        self.name = name

        return seg[all_e_idx+1:]

class Segment:
    def Parse(self, path:str) -> str:
        '''
        解析 seg
        '''
        if path.startswith('('):
            attr = Attr()
            path = attr.Parse()
        pass

    def __init__(self, seg:str) -> None:
        if seg.startswith('('):
            assert(seg.endswith(')'))
            seg = seg[1:-1]
        

        cond_s_idx = seg.find('[')
        self.conds:list[list[tuple(str,str)]] = []
        if cond_s_idx >= 0:
            cond_e_idx = seg.index(']')
        pass

class Path:
    def Parse(self, path:str) -> str:

        pass
    def __init__(self, path:str) -> None:
        segs = path.split('.')
        self.segs:list[Segment] = []
        for seg in segs:
            it = Segment(seg)
            self.segs.append(it)
        pass




def search(path:str,data):
    print("")


if __name__ == "__main__":
    print('test xpath')