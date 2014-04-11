using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class OdnoklassnikiWebplayerAPI : MonoBehaviour {
	
	#if UNITY_WEBPLAYER
	protected void CallApiMethod(Dictionary<string,string> parameters){
		Application.ExternalCall( "OKAPIWrapper.unity_api_call",  this.StringDictionaryToJSON(parameters));
	}
	
	protected virtual void APIMethodCallback(string param){
		this.LogToConsole ("API METHOD CALLBACK FROM UNITY: " + param);		
	}

	protected void JSgetPageInfo(){
		Application.ExternalCall ("FAPI.UI.getPageInfo");
	}

	protected void JSscrollToTop(){
		Application.ExternalCall ("FAPI.UI.scrollToTop");
	}

	protected void JSsetWindowSize(int width, int height){
		Application.ExternalCall ("FAPI.UI.setWindowSize", width, height);
	}

	protected void JSshowInvite(string text,string parameters, string selected_uids){
		Application.ExternalCall ("FAPI.UI.showInvite", text, parameters, selected_uids);
	}

	protected void JSshowNotification(string text,string parameters, string selected_uids){
		Application.ExternalCall ("FAPI.UI.showNotification", text, parameters, selected_uids);
	}

	// TODO uiConf (now not supported)
	protected void JSshowPayment(string name, string description, string code, int price, string options, string attributes, string currency, string callback){
		Application.ExternalCall ("FAPI.UI.showPayment", name, description, code, price,options,attributes, currency, callback);
	}

	protected void JSshowPermissions(List<string> permissions){
		StringBuilder builder = new StringBuilder ();
		builder.Append("[");
		foreach (string permission in permissions) {
			builder.Append("\"").Append(permission).Append("\",");
		}
		builder.Replace (",", "]", builder.Length - 1, 1);
		Application.ExternalCall ("FAPI.UI.showPermissions", builder.ToString());
	}

	protected virtual void JSMethodCallback(string param){
		this.LogToConsole ("JS METHOD CALLBACK FROM UNITY: " + param);	
	}
	
	protected void LogToConsole(string param){
		Application.ExternalCall("console.log", param);
	}
	#endif
	
	private string StringDictionaryToJSON(Dictionary<string,string> parameters){
		StringBuilder builder = new StringBuilder ();
		builder.Append ("{");
		foreach (KeyValuePair<string,string> pair in parameters) {
			builder.Append("\"").Append(pair.Key).Append("\":\"").Append(pair.Value).Append("\",");
		}
		builder.Replace (",", "}", builder.Length - 1, 1);
		return builder.ToString ();
	}
}
