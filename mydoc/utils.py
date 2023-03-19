import os
from urllib.request import urlopen
import json

WorkDir = os.path.abspath(os.path.dirname(__file__))

def get_model_jsonstr()->str:
    # url = 'https://microsoft.github.io/language-server-protocol/specifications/lsp/3.17/metaModel/metaModel.json'
    url = f"file:///{WorkDir}/metaModel.json"
    res = urlopen(url)
    jsonstr = res.read()
    return jsonstr

def get_model_json()->dict:
    jsonstr = get_model_jsonstr()
    data = json.loads(jsonstr)
    return data