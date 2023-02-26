'''
Path example:
- a.b.c
- a.1.c
- a..c
- a[x=1 & y=2 , z=3 ].(p1,p2)
- a.*.c | 1

Syntax 0.1 very simple:
rootpath := path { '|' path}
path := seg {'.' seg}
cond := Name=Name {'&' Name=Name }
attr := ( Name | Number | '' | '*' ) [ '[' cond {',' cond}  ']' ]
seg := attr | '(' attr {',' attr} ')'
'''

class Attr:
    def Parse(self,seg:str) -> str:
        '''
        解析attr, 返回剩余的字符串, 理论上应该是空串或者 , 开头的串
        '''
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