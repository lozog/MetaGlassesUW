
using System.IO;
using System.Xml;
using System.Net;


private static string RemoveWhiteSpace(string input){
	string result = "";
	int l=input.Length;
	for (int i=0; i<l; ++i){
		if (input[i] != ' '){
			result += input[i];
		} 
	}
	return result;
}


/*
string input=RemoveWhiteSpace(asdnasudnasoidand);


/
string part1 ="http://api.wolframalpha.com/v2/query?input=";
string part2 ="&appid=PG9HJR-4G89HAHLUH&output=xml";

derivativeof2x

string result = part1 + input + part2; */


//get and parse wolfram API for the answer
/*private static string WolframAnswer(var xmlFile){
	int l=xmlFile.Length;
	for (int i=0; i<l; ++i){
		for (int j=1; j<i+1;++j){
			for (int k=2; k<j+1;++k){
				string newStr = xmlFile[i] + xmlFile[j] + xmlFile[k];
				if (newStr == "alt"){
					i+=4; 
				}
			}
		}
	}
	string result="";
	while (i != '"'){
		if (xmlFile[i] == '='){
			++i;
		} else if (xmlFile[i]==' '){
			++i;
		} else {
			result+=xmlFile[i];
			++i;
		}
	}
	return result; 
}*/

private static string WolframAnswer(XDocument xmlFile){
	int l=xmlFile.Length;
	string equation = xmlFile.SelectSingleNode("plaintext").InnerText;
	string result="";
	while (i != '"'){
		if (xmlFile[i] == '='){
			++i;
		} else if (xmlFile[i]==' '){
			++i;
		} else {
			result+=xmlFile[i];
			++i;
		}
	}
	return result; 
}






//power conversion testing
  private static string WPowConversion(string answer){
	int l = answer.Length;
	int i=0;
	string newAnswer="";
	while (i<l){
	    
		if ((i+1 < l)&&(answer[i+1] == '^')){
			newAnswer = newAnswer + "*Mathf.Pow(" + answer[i] + "," + answer[i+2] + ")";
			
		} else {
			newAnswer += answer[i];
		}
		++i;
	}   
	return newAnswer;

    }



   


//trig conversion
private static string WAnswerConversion(string answer){
	
	int l = answer.Length;
	string answer2 = WPowConversion(answer);
	string newAnswer="";
	string newAnswer2="";
	const string sine = Mathf.
	int i=0;
	int j=0;
	while(i<l){
		if(answer[i]=='s'){
			i+=3;
			newAnswer= newAnswer + "Mathf.Sin(" + answer2[i] + ")";
			++i;
			for (i; i<l; ++i){
				newAnswer2[j] = answer2[i];
				++j;
			}
			newAnswer += newAnswer2;
			break;
		} else if(answer[i]=='c'){
			i+=3;
			newAnswer= newAnswer + "Mathf.Cos(" + answer2[i] + ")";
			++i;
			for (i; i<l; ++i){
				newAnswer2[j] = answer2[i];
				++j;
			}
			newAnswer += newAnswer2;
			break;
		} else {
			++i;
		}

	}
	return answer2;

}




