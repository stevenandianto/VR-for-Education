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

public class ChooseMateri : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IPAClicked (){
		PlayerPrefs.SetString ("subject", "IPA");
		PlayerPrefs.Save ();
		Debug.Log("SUBJEK"+PlayerPrefs.GetString("subject"));
		materi_download ();
		LoadScene ("SciFi Level");
	}

	public void IPSClicked (){
		PlayerPrefs.SetString ("subject", "IPS");
		PlayerPrefs.Save ();
		//materi_download ();
		//LoadScene ();
	}

	public void downloader(string subjek){
		Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
		Firebase.Storage.StorageReference storage_ref = storage.GetReferenceFromUrl ("gs://testing-7cdc4.appspot.com");
		Firebase.Storage.StorageReference images_ref = storage_ref.Child ("Images/"+subjek+"/Cover");
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		List<string> firebasefilenames = new List<string>();
		List<string> filenames = new List<string>();
		//Check File Un
		if (!Directory.Exists (Application.dataPath.ToString () + "/Resources/Images/IPA/Cover")) {
			Directory.CreateDirectory (Application.dataPath.ToString () + "/Resources/Images/IPA/Cover");
		} else {
			DirectoryInfo dir = new DirectoryInfo (Application.dataPath.ToString () + "/Resources/Images/IPA/Cover");
			FileInfo[] info = dir.GetFiles ("*.*");

			foreach (FileInfo f in info) {
				if (!f.Name.Contains ("meta")) {
					filenames.Add(f.Name);
				}
			}
		}
		//Check Firebase Database stored Cover image
		FirebaseDatabase.DefaultInstance
			.GetReference("gambar").Child(PlayerPrefs.GetString("subject")).Child("Cover")
			.ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
			if (e2.DatabaseError != null) {
				Debug.Log(e2.DatabaseError.Message);
				}
			if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
				foreach (var childSnapshot in e2.Snapshot.Children){
					string filename = childSnapshot.Child("filename").Value.ToString();
					firebasefilenames.Add(filename);
				}
				//If file unity ada yang berbeda dengan firebase di buang
				for(int i=0; i<filenames.Count; i++){
					if(firebasefilenames.Contains(filenames[i])){
							Debug.Log("FILE SAMA");
					} else {
						File.Delete(Application.dataPath.ToString () + "/Resources/Images/IPA/Cover/" + filenames[i]);
					}
				}
				//If file database ada yang baru dari unity maka di download
				for(int i=0; i<firebasefilenames.Count; i++){
					if(filenames.Contains(firebasefilenames[i])){
						Debug.Log("FILE SAMA");
					} else {
						Firebase.Storage.StorageReference cover_ref = images_ref.Child (firebasefilenames[i]);
						string resource_path = Application.dataPath.ToString() + "/Resources/Images/IPA/Cover/"+firebasefilenames[i];
						// Start downloading a file
						Task task = cover_ref.GetFileAsync (resource_path);
						task.ContinueWith (resultTask => {
							if (!resultTask.IsFaulted && !resultTask.IsCanceled) {
								Debug.Log ("Download finished.");
							}
						});
					}
				}
			}
			};
		//Debug.Log ("PATH FIREBASE" + images_ref.Child(subjek));
	}


	public void materi_download(){
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		FirebaseDatabase.DefaultInstance
			.GetReference("materi")
			.GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted) {
					Debug.Log("Database Fault");
				}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					PlayerPrefs.SetInt ("materi_size", (int) snapshot.ChildrenCount);
					PlayerPrefs.Save ();
					Debug.Log("MATERI SIZE = "+PlayerPrefs.GetInt("materi_size"));
					//retrieve_judul((int) snapshot.ChildrenCount);
					//retrieve_deskripsi((int) snapshot.ChildrenCount);
					Debug.Log("SUBJEK ="+PlayerPrefs.GetString("subject"));
					downloader(PlayerPrefs.GetString("subject"));
					//downloaderContent();
					//downloaderContent(PlayerPrefs.GetString("subject"),2);
					
				}
			});

	}

	void LoadScene(String scene){
		Application.LoadLevel (scene);
	}

	public void LoadMateriScene (){
		Application.LoadLevel ("MateriScene");
	}
		
}
