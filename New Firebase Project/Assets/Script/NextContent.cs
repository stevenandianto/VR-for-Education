using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using System.Threading.Tasks;
using Firebase.Storage;
using Firebase.Unity.Editor;
using System;
using System.Threading;
using Firebase.Database;

public class NextContent : MonoBehaviour {

	public GameObject gameObject;
	public Text title;
	public Text description;
	int i = 0;
	UnityEngine.Object[] textures;
	string[] titles;
	string[] descriptions;
	public void Start(){
		Debug.Log("CONTENT SIZE NEXT CONTENT = "+PlayerPrefs.GetInt("content_size"));
		Debug.Log("SUBJEK NEXT CONTENT="+PlayerPrefs.GetString("subject"));
		retrieve_judul(PlayerPrefs.GetInt("content_size"));
		retrieve_deskripsi(PlayerPrefs.GetInt("content_size"));
		textures = Resources.LoadAll ("Images/IPA/"+PlayerPrefs.GetInt("materi_id"));
		//Debug.Log ("TEXTURES LENGTH ="+textures.Length);
		titles = new string[PlayerPrefs.GetInt("content_size")];
		descriptions = new string[PlayerPrefs.GetInt("content_size")];
		renderer (i);
		//Debug.Log ("TITLES LENGTH ="+titles.Length);
	}
	public void FixedUpdate(){
		renderer (i);
	}
	// OnClick button
	public void onClick () {
		Debug.Log ("Next Button Clicked");	
		i++;
		if (i == textures.Length) {
			i = 0;
		}
		renderer (i);
		Debug.Log ("Texture ke-"+i);
	}

	public void renderer (int i){
		Renderer renderer = gameObject.GetComponent<Renderer>();
		Texture texture = (Texture)textures [i];
		renderer.material.mainTexture = texture;
		title.text = titles [i];
		description.text = descriptions [i];
		PlayerPrefs.SetInt ("content_id", i+1);
		PlayerPrefs.Save ();
	}

	public void retrieve_judul(int size){
		int arrindeks = 0;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		titles = new string[size];
		for (int i = 1; i <= size; i++) {
			FirebaseDatabase.DefaultInstance
				.GetReference ("materi/"+i+"/materi_judul")
				.GetValueAsync ().ContinueWith (task => {
					if (task.IsFaulted) {
						Debug.Log ("Database Fault");
					} else if (task.IsCompleted) {
						DataSnapshot snapshot = task.Result;
						Debug.Log ("Database Snapshot" + snapshot.Value);
						titles[arrindeks] = snapshot.Value.ToString();
						arrindeks++;
					}
				});
		}
	}

	public void retrieve_deskripsi(int size){
		int arridx = 0;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		descriptions = new string[size];
		for (int i = 1; i <= size; i++) {
			FirebaseDatabase.DefaultInstance
				.GetReference ("materi/"+i+"/materi_deskripsi")
				.GetValueAsync ().ContinueWith (task => {
					if (task.IsFaulted) {
						Debug.Log ("Database Fault");
					} else if (task.IsCompleted) {
						DataSnapshot snapshot = task.Result;
						Debug.Log ("Database Snapshot" + snapshot.Value);
						descriptions[arridx] = snapshot.Value.ToString();
						arridx++;
					}
				});
		}
	}

}