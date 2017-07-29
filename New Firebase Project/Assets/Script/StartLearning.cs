using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System.IO;
using System.Threading.Tasks;


public class StartLearning : MonoBehaviour {
	// OnClick button
	public void onClick () {
		Debug.Log ("Start Button Clicked");	
		Application.LoadLevel ("ContentScene");
		downloaderContent ();
		Debug.Log ("Player Pref" + PlayerPrefs.GetInt ("materi_id"));
	}

	public void downloaderContent(){
		Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
		Firebase.Storage.StorageReference storage_ref = storage.GetReferenceFromUrl ("gs://testing-7cdc4.appspot.com");
		Firebase.Storage.StorageReference images_ref = storage_ref.Child ("Images/"+PlayerPrefs.GetString("subject"));
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://testing-7cdc4.firebaseio.com/");
		DatabaseReference dbref = FirebaseDatabase.DefaultInstance.RootReference;
		int m = PlayerPrefs.GetInt("materi_id");
		//Check File Unity
		List<string> firebasefilenames = new List<string> ();
		List<string> filenames = new List<string> ();
		if (!Directory.Exists (Application.dataPath.ToString () + "/Resources/Images/IPA/" + m.ToString ())) {
			Directory.CreateDirectory (Application.dataPath.ToString () + "/Resources/Images/IPA/" + m.ToString ());
		} else {
			DirectoryInfo dir = new DirectoryInfo (Application.dataPath.ToString () + "/Resources/Images/IPA/" + m.ToString ());
			FileInfo[] info = dir.GetFiles ("*.*");

			foreach (FileInfo f in info) {
				if (!f.Name.Contains ("meta")) {
					filenames.Add (f.Name);
				}
			}
			PlayerPrefs.SetInt ("content_size", filenames.Count);
		}
		//Check Firebase Database stored content image
		FirebaseDatabase.DefaultInstance
			.GetReference ("gambar").Child (PlayerPrefs.GetString ("subject")).Child ("Content").Child (m.ToString ())
			.ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
			if (e2.DatabaseError != null) {
				Debug.Log (e2.DatabaseError.Message);
			}
			if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
				foreach (var childSnapshot in e2.Snapshot.Children) {
					string filename = childSnapshot.Child ("filename").Value.ToString ();
					firebasefilenames.Add (filename);
				}
				//If file unity ada yang berbeda dengan firebase di buang
				for (int j = 0; j < filenames.Count; j++) {
					if (firebasefilenames.Contains (filenames [j])) {
						Debug.Log ("FILE SAMA");
					} else {
						Debug.Log ("MASUK SINI");
						Debug.Log ("FILE BUANG" + filenames [j]);
						Debug.Log (m);
						Debug.Log ("PATH BUANG" + Application.dataPath.ToString () + "/Resources/Images/IPA/" + m.ToString () + "/" + filenames [j]);
						File.Delete (Application.dataPath.ToString () + "/Resources/Images/IPA/" + m.ToString () + "/" + filenames [j]);
					}
				}
				//If file database ada yang baru dari unity maka di download
				for (int k = 0; k < firebasefilenames.Count; k++) {
					if (filenames.Contains (firebasefilenames [k])) {
						Debug.Log ("FILE SAMA");
					} else {
						Debug.Log ("MASUK SINI A");
						Firebase.Storage.StorageReference cover_ref = images_ref.Child (m.ToString ()).Child (firebasefilenames [k]);
						Debug.Log (cover_ref);
						string resource_path = Application.dataPath.ToString () + "/Resources/Images/IPA/" + m.ToString () + "/" + firebasefilenames [k];
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
	}
}
