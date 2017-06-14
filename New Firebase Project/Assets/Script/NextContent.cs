using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NextContent : MonoBehaviour {

	public GameObject gameObject;
	public Text title;
	public Text description;
	int i = 0;
	Object[] textures;
	string[] titles;
	string[] descriptions;


	public void Start(){
		Debug.Log ("PLAYERPREF = "+PlayerPrefs.GetInt ("materi_id"));
		textures = Resources.LoadAll ("Images/IPA/"+PlayerPrefs.GetInt("materi_id"));
		Debug.Log ("TEXTURES LENGTH ="+textures.Length);
		titles = new string[textures.Length];
		descriptions = new string[textures.Length];
		for (int j = 0; j < titles.Length; j++) {
			int k = j + 1;
			titles [j] = "Judul - "+k.ToString ();
			descriptions [j] = "Deskripsi -"+k.ToString ();
		}
		renderer (i);
		Debug.Log ("TITLES LENGTH ="+titles.Length);
	}

	// OnClick button
	public void onClick () {
		Debug.Log ("Next Button Clicked");	
		Debug.Log ("Texture ke-"+i);
		i++;
		if (i == textures.Length) {
			i = 0;
		}
		renderer (i);
	}

	public void renderer (int i){
		Renderer renderer = gameObject.GetComponent<Renderer>();
		Texture texture = (Texture)textures [i];
		renderer.material.mainTexture = texture;
		title.text = titles [i];
		description.text = descriptions [i];
		PlayerPrefs.SetInt ("materi_id", i+1);
		PlayerPrefs.Save ();
	}
}