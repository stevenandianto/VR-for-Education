using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyCharacter : MonoBehaviour {

	public GameObject fairy1;

	public GameObject fairy2;

	public GameObject fairy3;
	// Use this for initialization
	void Start () {
		Debug.Log (PlayerPrefs.GetInt ("fairyid"));
		if (PlayerPrefs.GetInt ("fairyid") == 1) {
			fairy1.SetActive (true);
		} else if (PlayerPrefs.GetInt ("fairyid") == 2) {
			fairy2.SetActive (true);
		} else if (PlayerPrefs.GetInt ("fairyid") == 3) {
			fairy3.SetActive (true);
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
