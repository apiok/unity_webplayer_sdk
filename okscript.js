OKAPIWrapper = {
    unityObject : null,
    processAPIIniterror : function(errorObject) {
        console.log("OK API Init error");
    },
    init : function (unityobject){
        OKAPIWrapper.unityObject = unityobject;
        var rParams = FAPI.Util.getRequestParameters();
        FAPI.init(rParams["api_server"], rParams["apiconnection"],
                  function() {},
                  function(error){
                      OKAPIWrapper.processAPIIniterror(error);
                  });
    },
    unity_api_call : function(parameters){
        if (!(OKAPIWrapper.unityObject && OKAPIWrapper.unityObject.getUnity !== undefined && OKAPIWrapper.unityObject.getUnity().SendMessage !== undefined)){
            console.log("OKAPIWrapper is not initsialized or no unityObject passed to OKAPIWrapper. If you are using internet explorer, is'possible, that script works correct, please, check it in debugger.");
        }
        FAPI.Client.call(JSON.parse(parameters), OKAPIWrapper.unity_api_callback);
    },
    unity_api_callback: function(method,result,data){
        OKAPIWrapper.unityObject.getUnity().SendMessage("OKAPI", "APIMethodCallback", JSON.stringify(result));
    }
}

function API_callback(method, result, data) {
    var rez = {"method":method, "result":result, "data":data};
    OKAPIWrapper.unityObject.getUnity().SendMessage("OKAPI", "JSMethodCallback", JSON.stringify(rez));
}

function getUrlVars()
{
    var vars = new Object(), hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for(var i = 0; i < hashes.length; i++)
    {
        hash = hashes[i].split('=');
        vars[hash[0]] = hash[1];
    }
    OKAPIWrapper.unityObject.getUnity().SendMessage("OKAPI", "GetUrlVarsCallback", JSON.stringify(vars));
}