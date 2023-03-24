import os
from urllib.request import urlopen
import jinja2

ja = jinja2.Environment(loader=jinja2.FileSystemLoader("./templates"), lstrip_blocks=True, trim_blocks = True)

import json
import xpath
import utils

WorkDir = os.path.abspath(os.path.dirname(__file__))

def mod_typeinfo(js:dict):
    kind = js['kind']
    match kind:
        case 'or':
            tmap:dict = {}
            
            js['cs_typename'] = 'MyNode'

            pass
        case 'reference':
            js['cs_typename'] = js['name']
            pass
        case 'array':
            mod_typeinfo(js['element'])
            js['cs_typename'] = f"List<{js['element']['cs_typename']}>"
            pass
        case 'base':
            match js['name']:
                case 'null':
                    js['is_null'] = True
                    pass
                case 'DocumentUri':
                    js['cs_typename'] = 'DocumentUri'
                    pass
                case 'URI':
                    js['cs_typename'] = 'Uri'
                    pass
                case 'string':
                    js['cs_typename'] = 'string'
                    pass
                case 'boolean':
                    js['cs_typename'] = 'bool'
                    pass
                case 'uinteger':
                    js['cs_typename'] = 'uint'
                    pass
                case 'integer':
                    js['cs_typename'] = 'int'
                    pass
                case 'decimal':
                    # 有毒。值在Color的几个分量上用了一下
                    js['cs_typename'] = 'float'
                    pass
                case _:
                    raise Exception(f"unkown base type: {js['name']}")
                    pass
            pass
        case 'and':
            # 只有 method textDocument/colorPresentation 使用了。也许有什么用吧
            js['cs_typename'] = 'MyNode'
            pass
        case 'map':
            pass
        case 'literal':
            pass
        case 'stringLiteral':
            pass
        case 'tuple':
            return "MyNode"
            pass


def gen_structs(items):
    tm = ja.get_template("structs.cs.j2")
    content = tm.render(items=items[:3])
    print(content)

if __name__ == "__main__":
    data = utils.get_model_json()
    gen_structs(data["structures"])
    pass
