using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class OdnoklassnikiWebplayerAPI : MonoBehaviour {
	
	#if UNITY_WEBPLAYER
	protected void callApiMethod(Dictionary<string,string> parameters){
		Application.ExternalCall( "OKAPIWrapper.unity_api_call",  this.stringDictionaryToJSON(parameters));
	}
	
	protected virtual void webPlayerCallback(string param){
		this.logToConsole ("API CALLBACK FROM UNITY: " + param);
		
	}
	
	protected void logToConsole(string param){
		Application.ExternalCall("console.log", param);
	}
	#endif
	
	string stringDictionaryToJSON(Dictionary<string,string> parameters){
		StringBuilder builder = new StringBuilder ();
		builder.Append ("{");
		foreach (KeyValuePair<string,string> pair in parameters) {
			builder.Append("\"").Append(pair.Key).Append("\":\"").Append(pair.Value).Append("\",");
		}
		builder.Replace (",", "}", builder.Length - 1, 1);
		return builder.ToString ();
	}
}
