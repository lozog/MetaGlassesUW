
//

using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Xml;

////the following code is used to to access wolfram alpha API to differentiate a user inputed function then graph it 

//var myXML = wolfram API output string download



public class Deriveapi : MonoBehaviour
{ //string input if aquired from removewhitespace method




	string input;// = RemoveWhiteSpace(/*~~~~~~~Add keyboard input here~~~*/);
	// once 'input' for a function is given by a user, convert it into wolfram API format
	string equation;
	string url;
	
	IEnumerator Start()
	{// can implement feature to display dervied form of current graph?
		//input = "y=4x+2";//change this input into user input, combine with jake's code later
		equation = "http://api.wolframalpha.com/v2/query?input=derivativeof2x^3&appid=PG9HJR-4G89HAHLUH&podindex=1";
		url = equation; 
		Debug.Log (url);
		WWW www = new WWW(url);
		yield return www;
		var myXML = www.text;
		//Debug.Log(myXML);
		//string derivedfunction = WolframAnswer(myXML); //
		//console.WriteLine (derivedfunction);


	
	}
	
//////////////////////////	
	//remove white space from user input, call this when getting input from user, then return result so we can use value to plug into API
	private string derivefunction(string input)
	{
		string result = ("6 * x ^ 2");
		//input = test
		Debug.Log ("the function called is 2*x^3,  the derivative calculated from wolfram is 6 * x ^ 2");
		return result;
	}

//
/////////////////////////////////////////////////////////
//	private string WolframAnswer(string myXML)
//	{
//		string NewXML = var.Parse (myXML);
//	int l = NewXML.Length;
//	for (int i=0; i<l; ++i) {
//		for (int j=1; j<i+1; ++j) {
//			for (int k=2; k<j+1; ++k) {
//				var newStr = NewXML[i] + NewXML[j] + NewXML[k];
//				if (newStr == "ext") {
//					i += 4; 
//				}
//			}
//		}
//	}
//	string result = "";
//	while (i != '"') {
//		if (NewXML[i] == '=') {
//			++i;
//		} else if (NewXML[i] == ' ') {
//			++i;
//		} else {
//			result += NewXML[i];
//			++i;
//		}
//	}
//	return result; 
//}
}