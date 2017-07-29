using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text scoreboard;
	float score;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick(){
		if (PlayerPrefs.GetFloat ("score") == null) {
			score = 0;
		} else {
			score = PlayerPrefs.GetFloat ("score");
		}
		scoreboard.text = score.ToString();
	}
}
