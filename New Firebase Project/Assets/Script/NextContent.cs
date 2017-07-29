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

public class NextContent : MonoBehaviour {

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
	float learningtime;
	float playerscore = 0f;
	public void Start(){
		//Debug.Log("MATERI SIZE NEXT MATERI = "+PlayerPrefs.GetInt("materi_size"));
		//Debug.Log("SUBJEK NEXT MATERI="+PlayerPrefs.GetString("subject"));
		retrieve_image_list(PlayerPrefs.GetInt("content_size"));
		retrieve_judul(PlayerPrefs.GetInt("content_size"));
		retrieve_deskripsi(PlayerPrefs.GetInt("content_size"));
		ReadImageList ();
		temptextures = Resources.LoadAll("Images/"+PlayerPrefs.GetString("subject")+"/"+PlayerPrefs.GetInt("materi_id").ToString());
		textures = new List<UnityEngine.Object> ();
		for (int l = 0; l < imagenames.Length; l++) {
			for (int m = 0; m < temptextures.Length; m++) {
				if (temptextures [m].name == Path.GetFileNameWithoutExtension (imagenames [l])) {
					textures.Add(temptextures [m]);
				}
			}
		}
		models = Resources.LoadAll ("Models/IPA/"+PlayerPrefs.GetInt("materi_id").ToString()+"/Objects");
		//Debug.Log ("TEXTURES LENGTH ="+textures.Length);
		titles = new string[PlayerPrefs.GetInt("content_size")];
		descriptions = new string[PlayerPrefs.GetInt("content_size")];
		for (int l = 0; l < models.Length; l++) {
			if (models [l].name == (i + 1).ToString ()) {
				Instantiate (models [l], model3d.transform.position, Quaternion.identity, model3d.transform);
				modelcontainer.SetActive (true);
				model3d.SetActive (true);
			}
		}
		ReadInformasiKonten ();
		renderer (i);
		//Debug.Log ("TITLES LENGTH ="+titles.Length);
	}
	public void FixedUpdate(){
		learningtime += Time.deltaTime;
		renderer (i);
	}
	// OnClick button
	public void onClick () {
		modelcontainer.SetActive (false);
		Debug.Log ("Next Button Clicked");	
		if (learningtime > 20.0f) {
			playerscore = PlayerPrefs.GetFloat ("score");
			playerscore += 10f;
			PlayerPrefs.SetFloat ("score", playerscore);
		}
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
				Instantiate (models [l], model3d.transform.position, Quaternion.identity, model3d.transform);
			}
		}
		learningtime = 0;
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
	public void retrieve_image_list(int size){
		string path = Application.dataPath.ToString () + "/Resources/Information/"+PlayerPrefs.GetString("subject")+PlayerPrefs.GetInt("materi_id").ToString()+".txt";
		int imageindeks = 0;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		//titles = new string[size];
		for (int i = 1; i <= size; i++) {
			FirebaseDatabase.DefaultInstance
				.GetReference ("gambar/"+PlayerPrefs.GetString("subject")+"/Content/"+PlayerPrefs.GetInt("materi_id").ToString()+"/"+i+"/filename")
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
		string path = Application.dataPath.ToString () + "/Resources/Information/judulkonten"+PlayerPrefs.GetString("subject")+PlayerPrefs.GetInt("materi_id")+".txt";
		int arrindeks = 0;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		titles = new string[size];
		for (int i = 1; i <= size; i++) {
			FirebaseDatabase.DefaultInstance
				.GetReference ("materi/"+PlayerPrefs.GetInt("materi_id")+"/konten/"+i+"/konten_judul")
				.GetValueAsync ().ContinueWith (task => {
					if (task.IsFaulted) {
						Debug.Log ("Database Fault");
					} else if (task.IsCompleted) {
						DataSnapshot snapshot = task.Result;
						Debug.Log ("Database Snapshot JUDUL" + snapshot.Value);
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
		string path = Application.dataPath.ToString () + "/Resources/Information/deskripsikonten"+PlayerPrefs.GetString("subject")+PlayerPrefs.GetInt("materi_id")+".txt";
		Debug.Log ("/Resources/Information/deskripsikonten" + PlayerPrefs.GetString ("subject") + PlayerPrefs.GetInt ("materi_id") + ".txt");
		int arridx = 0;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		descriptions = new string[size];
		for (int i = 1; i <= size; i++) {
			FirebaseDatabase.DefaultInstance
				.GetReference ("materi/"+PlayerPrefs.GetInt("materi_id")+"/konten/"+i+"/konten_deskripsi")
				.GetValueAsync ().ContinueWith (task => {
					if (task.IsFaulted) {
						Debug.Log ("Database Fault");
					} else if (task.IsCompleted) {
						DataSnapshot snapshot = task.Result;
						Debug.Log ("Database Snapshot DESKRIPSI" + snapshot.Value);
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
	public void ReadInformasiKonten(){
		TextAsset judul = Resources.Load ("Information/judulkonten"+PlayerPrefs.GetString("subject")+PlayerPrefs.GetInt("materi_id")) as TextAsset;
		TextAsset deskripsi = Resources.Load ("Information/deskripsikonten"+PlayerPrefs.GetString("subject")+PlayerPrefs.GetInt("materi_id")) as TextAsset;
		titles = judul.text.Split ("\n" [0]);
		descriptions = deskripsi.text.Split ("\n" [0]);
	}
	public void ReadImageList(){
		TextAsset imagename = Resources.Load ("Information/" + PlayerPrefs.GetString ("subject") + PlayerPrefs.GetInt ("materi_id").ToString ()) as TextAsset;
		imagenames = imagename.text.Split ("\n" [0]);
	}

}
