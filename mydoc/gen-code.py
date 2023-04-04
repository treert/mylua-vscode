import os
from urllib.request import urlopen
import jinja2

ja = jinja2.Environment(loader=jinja2.FileSystemLoader("./templates"), lstrip_blocks=True, trim_blocks = True)

import json
import xpath
import utils

WorkDir = os.path.abspath(os.path.dirname(__file__))

MyLiteralTypeIdx = 1
def mod_typeinfo(js:dict):
    if not js:
        return
    kind = js['kind']
    match kind:
        case 'or':
            tmap:dict = {}
            sub_types:list[str] = []
            can_be_null = False
            for it in js['items']:
                mod_typeinfo(it)
                if it.get('is_null', False):
                    can_be_null = True
                else:
                    sub_types.append(it['cs_typename'])
            
            js['cs_typename'] = 'MyNode'
            if can_be_null:
                js['or_type_list_doc'] = f"[null,{','.join(sub_types)}]"
            else:
                js['or_type_list_doc'] = f"[{','.join(sub_types)}]"

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
                    js['cs_typename'] = 'MyNode'
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
            # 只有 method textDocument/colorPresentation 的 registrationOptions 里使用了。
            # 也许有什么用吧，现在对我没有用
            js['cs_typename'] = 'MyNode'
            pass
        case 'map':
            # 只有 6 个
            mod_typeinfo(js['key'])
            mod_typeinfo(js['value'])
            js['cs_typename'] = f"Dictionary<{js['key']['cs_typename']},{js['value']['cs_typename']}>"
            pass
        case 'literal':
            # 有 54 个，其中 37 个不同。 太麻烦了，就简单处理吧
            js['cs_typename'] = 'MyNode'
            pass
        case 'stringLiteral':
            # const string, do nothing
            pass
        case 'tuple':
            # only one. current is int[2]
            js['cs_typename'] = 'MyNode'
            pass

def mod_struct(it):
    for prop in it['properties']:
        mod_typeinfo(prop['type'])

def mod_request(it):
    mod_typeinfo(it.get('params'))
    mod_typeinfo(it.get('result'))
    mod_typeinfo(it.get('partialResult'))

    mod_typeinfo(it.get('registrationOptions'))

    if it['messageDirection'] == 'clientToServer':
        it['cs_messageDirection'] = 'MessageDirection.ClientToServer'
    elif it['messageDirection'] == 'serverToClient':
        it['cs_messageDirection'] = 'MessageDirection.ServerToClient'
    else:
        it['cs_messageDirection'] = 'MessageDirection.Both'

def mod_notify(it):
    mod_typeinfo(it.get('params'))
    mod_typeinfo(it.get('registrationOptions'))

    if it['messageDirection'] == 'clientToServer':
        it['cs_messageDirection'] = 'MessageDirection.ClientToServer'
    elif it['messageDirection'] == 'serverToClient':
        it['cs_messageDirection'] = 'MessageDirection.ServerToClient'
    else:
        it['cs_messageDirection'] = 'MessageDirection.Both'

def mod_enum(it):
    mod_typeinfo(it['type'])

def mod_all_json(data):
    for it in data['requests']:
        mod_request(it)
    for it in data['notifications']:
        mod_notify(it)
    for it in data['structures']:
        mod_struct(it)
    for it in data['enumerations']:
        mod_enum(it)

def gen_structs(items):
    tm = ja.get_template("structs.cs.j2")
    content = tm.render(items=items[:3])
    print(content)

if __name__ == "__main__":
    data = utils.get_model_json()
    mod_all_json(data)

    jsondata = json.dumps(data, indent=2)
    path = os.path.join(WorkDir,"gen-code.json")
    
    with open(path,"w",encoding="utf-8") as file:
        file.write(jsondata)
    pass
