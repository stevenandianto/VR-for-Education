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
using System.IO;
using System.Text.RegularExpressions;

public class NextMateri : MonoBehaviour {

	public GameObject gameObject;
	public Text title;
	public Text description;
	public GameObject model3d;
	public GameObject modelcontainer;
	int i = 0;
	List<UnityEngine.Object> textures;
	UnityEngine.Object[] temptextures;
	UnityEngine.Object[] models;
	string[] titles;
	string[] descriptions;
	string[] imagenames;
	public void Start(){
		Debug.Log("MATERI SIZE NEXT MATERI = "+PlayerPrefs.GetInt("materi_size"));
		Debug.Log("SUBJEK NEXT MATERI="+PlayerPrefs.GetString("subject"));
		if (PlayerPrefs.GetFloat ("score") == null) {
			PlayerPrefs.SetFloat ("score", 0);
		}
		retrieve_image_list(PlayerPrefs.GetInt("materi_size"));
		retrieve_judul(PlayerPrefs.GetInt("materi_size"));
		retrieve_deskripsi(PlayerPrefs.GetInt("materi_size"));
		ReadImageList ();
		temptextures = Resources.LoadAll ("Images/IPA/Cover");
		textures = new List<UnityEngine.Object> ();
		for (int l = 0; l < imagenames.Length; l++) {
			for (int m = 0; m < temptextures.Length; m++) {
				if (temptextures [m].name == Path.GetFileNameWithoutExtension (imagenames [l])) {
					textures.Add(temptextures [m]);
				}
			}
		}
		models = Resources.LoadAll ("Models/IPA/Cover/Objects");
		Debug.Log ("LENGTH" + models.Length);
		//Debug.Log ("TEXTURES LENGTH ="+textures.Length);
		titles = new string[PlayerPrefs.GetInt("materi_size")];
		descriptions = new string[PlayerPrefs.GetInt("materi_size")];
		for (int l = 0; l < models.Length; l++) {
			if (models [l].name == (i + 1).ToString ()) {
				Instantiate (models [l], model3d.transform.position, model3d.transform.rotation, model3d.transform);
			}
		}
		ReadInformasiMateri ();
		renderer (i);
		//Debug.Log ("TITLES LENGTH ="+titles.Length);
	}
	public void FixedUpdate(){
		renderer (i);
	}
	// OnClick button
	public void onClick () {
		modelcontainer.SetActive (false);
		Debug.Log ("Next Button Clicked");
		if (model3d.transform.childCount > 0) {
			Destroy (model3d.transform.GetChild (0).gameObject);
		}
		i++;
		if (i == textures.Count) {
			i = 0;
		}
		renderer (i);
		for (int l = 0; l < models.Length; l++) {
			if (models [l].name == (i + 1).ToString ()) {
				modelcontainer.SetActive (true);
				Instantiate (models [l], model3d.transform.position, model3d.transform.rotation, model3d.transform);
			}
		}
		Debug.Log ("Texture ke-"+i);
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
	public void retrieve_image_list(int size){
		string path = Application.dataPath.ToString () + "/Resources/Information/"+PlayerPrefs.GetString("subject")+"Cover.txt";
		int imageindeks = 0;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		//titles = new string[size];
		for (int i = 1; i <= size; i++) {
			FirebaseDatabase.DefaultInstance
				.GetReference ("gambar/"+PlayerPrefs.GetString("subject")+"/Cover/"+i+"/filename")
				.GetValueAsync ().ContinueWith (task => {
					if (task.IsFaulted) {
						Debug.Log ("Database Fault");
					} else if (task.IsCompleted) {
						DataSnapshot snapshot = task.Result;
						Debug.Log ("Database Snapshot JUDUL" + snapshot.Value);
						//titles[arrindeks] = snapshot.Value.ToString();
						if(imageindeks == 0){
							File.WriteAllText(path,snapshot.Value.ToString());
						} else {
							File.AppendAllText(path,"\n"+snapshot.Value.ToString());
						}
						imageindeks++;
					}
				});
		}
	}	
	public void retrieve_judul(int size){
		string path = Application.dataPath.ToString () + "/Resources/Information/judulmateri.txt";
		int arrindeks = 0;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		//titles = new string[size];
		for (int i = 1; i <= size; i++) {
			FirebaseDatabase.DefaultInstance
				.GetReference ("materi/"+i+"/materi_judul")
			.GetValueAsync ().ContinueWith (task => {
				if (task.IsFaulted) {
					Debug.Log ("Database Fault");
				} else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
						Debug.Log ("Database Snapshot" + snapshot.Value);
						//titles[arrindeks] = snapshot.Value.ToString();
						if(arrindeks == 0){
							File.WriteAllText(path,snapshot.Value.ToString());
						} else {
							File.AppendAllText(path,"\n"+snapshot.Value.ToString());
						}
						arrindeks++;
				}
			});
		}
	}

	public void retrieve_deskripsi(int size){
		string path = Application.dataPath.ToString () + "/Resources/Information/deskripsimateri.txt";
		int arridx = 0;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		//descriptions = new string[size];
		for (int i = 1; i <= size; i++) {
			FirebaseDatabase.DefaultInstance
				.GetReference ("materi/"+i+"/materi_deskripsi")
				.GetValueAsync ().ContinueWith (task => {
					if (task.IsFaulted) {
						Debug.Log ("Database Fault");
					} else if (task.IsCompleted) {
						DataSnapshot snapshot = task.Result;
						Debug.Log ("Database Snapshot" + snapshot.Value);
						//descriptions[arridx] = snapshot.Value.ToString();
						if(arridx == 0){
							File.WriteAllText(path,snapshot.Value.ToString());
						} else {
							File.AppendAllText(path,"\n"+snapshot.Value.ToString());
						}
						arridx++;
					}
				});
		}
	}
	public void ReadInformasiMateri(){
		TextAsset judul = Resources.Load ("Information/judulmateri") as TextAsset;
		TextAsset deskripsi = Resources.Load ("Information/deskripsimateri") as TextAsset;
		titles = judul.text.Split ("\n" [0]);
		descriptions = deskripsi.text.Split ("\n" [0]);
	}

	public void ReadImageList(){
		TextAsset imagename = Resources.Load ("Information/"+PlayerPrefs.GetString("subject")+"Cover") as TextAsset;
		imagenames = imagename.text.Split ("\n" [0]);
	}

}