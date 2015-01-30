using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Net;

////the following code is used to to access wolfram alpha API to differentiate a user inputed function then gaph it 


//1st :get userinput for a function to derive , or get it from our pre-loaded graphs


//var myXML = wolfram API output string download


public class Differentiator : MonoBehaviour {
	//remove white space from user input
	private static string RemoveWhiteSpace(string input ){
		string result = "";
		int l= input.Length;
		int b=0;
		for (int i=0; i<l; ++i){
			if (input[i]!=' '){
				result[b] = input[i];
				++i;
				++b;
			} else {
				++i;
			}
		}
	}

		 string equation = "http://api.wolframalpha.com/v2/query?input=derivativeof"+input+"&appid=PG9HJR-4G89HAHLUH&output=xml";
		 string url = equation;

    IEnumerator Start() {
        WWW www = new WWW(url);
        yield return www;
        var myXML = www.data;
        
        //df = parsed output from API xml
    }

	//Derived function used to return value for mapping on graph
	public float Deriver () {

		return //df, eg: 5*x*x+c  }
}


