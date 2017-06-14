using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartLearning : MonoBehaviour {
	// OnClick button
	public void onClick () {
		Debug.Log ("Start Button Clicked");	
		Application.LoadLevel ("ContentScene");
		Debug.Log ("Player Pref" + PlayerPrefs.GetInt ("materi_id"));
	}
}
