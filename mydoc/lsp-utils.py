from encodings import utf_8
import os
from urllib.request import urlopen
import yaml
import json
import re
from copy import deepcopy
import jmespath


WorkDir = os.path.abspath(os.path.dirname(__file__))

def convert_arr_to_map(arr:list[dict], key:str) -> dict:
    data:dict = {}
    for value in arr:
        t = value.copy()
        tk = t.pop(key)
        data[key+'/'+tk] = t
    return data

def json_to_yaml(jsondata) -> str:
    '''
    json object 转换成 yaml格式的 字符串
    '''
    data:dict = json.loads(jsondata)
    # 格式化下，提取下method
    info = deepcopy(data)
    info['requests'] = convert_arr_to_map(data['requests'], 'method')
    info['notifications'] = convert_arr_to_map(data['notifications'], 'method')
    info['structures'] = convert_arr_to_map(data['structures'], 'name')
    info['enumerations'] = convert_arr_to_map(data['enumerations'], 'name')
    info['typeAliases'] = convert_arr_to_map(data['typeAliases'], 'name')
    data = info

    yamldata = yaml.dump(data, indent=2, allow_unicode=True, canonical=False, width=100000, line_break='\n', sort_keys=False)
    return yamldata

def yaml_to_json(yamldata) -> str:
    '''
    yaml 字符串 转换成 json 字符串
    '''
    data = yaml.load(yamldata)
    jsondata = json.dumps(data, indent=2)
    return jsondata

def get_and_dump_meta_as_yaml():
    # url = 'https://microsoft.github.io/language-server-protocol/specifications/lsp/3.17/metaModel/metaModel.json'
    url = f"file:///{WorkDir}/metaModel.json"
    res = urlopen(url)
    jsonstr = res.read()    
    yamlstr = json_to_yaml(jsonstr)
    # return
    lines = re.split("\n{3,}", yamlstr)
    lines = [t.replace('\n\n','\n') for t in lines]
    yamlstr =  '\n\n'.join(lines)

    path = os.path.join(WorkDir,"lsp-meta-model.yaml")
    
    with open(path,"w",encoding="utf-8") as file:
        file.write(yamlstr)

def get_model_jsonstr()->str:
    # url = 'https://microsoft.github.io/language-server-protocol/specifications/lsp/3.17/metaModel/metaModel.json'
    url = f"file:///{WorkDir}/metaModel.json"
    res = urlopen(url)
    jsonstr = res.read()
    return jsonstr



def print_debug_info():
    jsonstr = get_model_jsonstr()
    data:dict = json.loads(jsonstr)
    ret = jmespath.search("requests[].result.items | [*]",data)
    print(ret)

if __name__ == "__main__":
    print("start")
    print_debug_info()
    # get_and_dump_meta_as_yaml()