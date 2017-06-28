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

	public void downloader(string subjek, int size){
		Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
		Firebase.Storage.StorageReference storage_ref = storage.GetReferenceFromUrl ("gs://testing-7cdc4.appspot.com");
		Firebase.Storage.StorageReference images_ref = storage_ref.Child ("Images/"+subjek+"/Cover");

		Debug.Log ("PATH FIREBASE" + images_ref.Child(subjek));
		for (int l = 1; l <= size; l++) {
			Firebase.Storage.StorageReference cover_ref = images_ref.Child ("IPA_cover_"+l+".jpg");
			string resource_path = Application.dataPath.ToString() + "/Resources/Images/IPA/Cover/IPA_cover_" + l + ".jpg";
			// Start downloading a file
			Task task = cover_ref.GetFileAsync (resource_path);

			task.ContinueWith (resultTask => {
				if (!resultTask.IsFaulted && !resultTask.IsCanceled) {
					Debug.Log ("Download finished.");
				}
			});
		}
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
					downloader(PlayerPrefs.GetString("subject"),PlayerPrefs.GetInt("materi_size"));
				}
			});

	}
	IEnumerator MyLoadScene(float delay)
	{
		yield return new WaitForSeconds (delay);
		Application.LoadLevel ("MateriScene");
	}

	void LoadScene(String scene){
		Application.LoadLevel (scene);
	}

	public void LoadMateriScene (){
		StartCoroutine (MyLoadScene (3));
	}
		
}
