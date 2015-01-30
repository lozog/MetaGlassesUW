using UnityEngine;
using System.Collections;
using Meta;


public class HoldForMenu : MetaBehaviour {

	public GameObject MainMenu, selfref;

	// Use this for initialization
	void Start () {
		
	}

	
	// Update is called once per frame
	void Update () {
		Debug.Log("update.");
		if (Input.GetKey (KeyCode.Space)) {
			selfref.SetActive(false);
			MainMenu.SetActive(true);
		}
	}
}
