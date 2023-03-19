import os
from urllib.request import urlopen
import jinja2

ja = jinja2.Environment(loader=jinja2.FileSystemLoader("./templates"), lstrip_blocks=True, trim_blocks = True)

import json
import xpath
import utils

WorkDir = os.path.abspath(os.path.dirname(__file__))

def get_typename(js):
    kind = js['kind']
    match kind:
        case 'or':
            return "MyNode"
            pass
        case 'reference':
            return js['name']
            pass
        case 'array':
            return f"List<{get_typename(js['element'])}>"
            pass
        case 'base':
            pass
        case 'and':
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
