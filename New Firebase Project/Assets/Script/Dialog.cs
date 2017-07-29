using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

	public GameObject go;

	public GameObject anotherdialog;

	public GameObject canvas;


	public Animator animator;

	public Text name;

	public Text conversation;

	public Text thinking;

	private int triggercounter = 0;
	// Use this for initialization
	void Start () {

	}

	void closeDialog () {
		go.SetActive (false);
	}
	// Update is called once per frame
	void Update () {

	}

	public void MaidController (GameObject button) {
		animator.GetComponent<Animator> ();
		triggercounter++;
		button.SetActive (false);
		Debug.Log ("ELIZA COUNTER" + triggercounter);
		go.SetActive (false);
		if (PlayerPrefs.GetString ("subject") == "IPA") {
			if (triggercounter == 1) {
				animator.SetBool ("Bow", true);
				name.text = "Eliza Boskonovic";
				conversation.text = "Hai kau datang lagi..";
				anotherdialog.SetActive (true);
			} else if (triggercounter == 2) {
				animator.SetBool ("Idle", true);
				animator.SetBool ("Bow", false);
				name.text = "<color=magenta>Eliza Boskonovich</color>";
				conversation.text = "Perlu bantuan apa?";
				button.SetActive (true);
			} else {
				closeAnotherDialog ();
				triggercounter = 0;
			}
		} else {
			if (triggercounter == 1) {
				animator.SetBool ("Bow", true);
				name.text = "???";
				conversation.text = "Selamat datang di Perpustakaan 'Skypiea' dalam Dunia Para Peri...";
				anotherdialog.SetActive (true);
			} else if (triggercounter == 2) {
				animator.SetBool ("Idle", true);
				animator.SetBool ("Bow", false);
				name.text = "<color=magenta>Eliza Boskonovich</color>";
				conversation.text = "Perkenalkan, namaku <color=magenta>Eliza Boskonovich</color>. Kamu bisa memanggil aku Eliza. Aku adalah pengurus perpustakaan ini dan aku siap membantu jika kamu membutuhkan bantuan!";
			} else if (triggercounter == 3) {
				conversation.text = "Di dalam perpustakaan ini kamu bisa belajar dan mendapatkan pengalaman yang menarik. Untuk mulai belajar cobalah pergi ke arah sana dan carilah <color=yellow><B>Buku Ajaib</B></color> yang membawa kamu ke <color=green><B>dunia pengetahuan</B></color>!";
				animator.SetBool ("Point", true);
			} else if (triggercounter > 3) {
				closeAnotherDialog ();
				triggercounter = 0;
			}
		}
	}

	public void CaptainController(GameObject button){
		animator.GetComponent<Animator> ();
		triggercounter++;
		Debug.Log ("Captain COUNTER" + triggercounter);
		go.SetActive (false);
		if (PlayerPrefs.GetString ("subject") == "IPA") {
			if (triggercounter == 1) {
				name.text = "<color=blue>Capt. Roger D Teach</color>";
				conversation.text = "Hai prajurit kamu tersesat?";
				anotherdialog.SetActive (true);
			} else if (triggercounter == 2) {
				animator.SetBool ("Idle", true);
				animator.SetBool ("Idle2", false);
				name.text = "Capt. Roger D Teach";
				conversation.text = "Ada yang bisa kubantu??";
				button.SetActive (true);
			} else {
				closeAnotherDialog ();
				triggercounter = 0;
			}
		} else { 
			if (triggercounter == 1) {
				name.text = "???";
				conversation.text = "Selamat datang di Laboratorium Masa Depan 'Sci-Fi Laboratory'...";
				anotherdialog.SetActive (true);
			} else if (triggercounter == 2) {
				name.text = "<color=blue>Capt. Roger D Teach</color>";
				conversation.text = "Namaku <color=blue>Roger D Teach</color>. Aku adalah kapten di lab ini. Kamu bisa panggil aku Capt.Roger.\n Datanglah kepadaku jika perlu bantuan!";
				animator.SetBool ("Idle2", true);
			} else if (triggercounter == 3) {
				conversation.text = "Aku memberikanmu izin untuk berkeliling di lab ini. Ikutilah tanda yang ada di dinding untuk pergi ke <color=yellow><B>Ruang Belajar</B></color>. Disana akan ada <color=green><B>Peri</B></color> yang membantumu belajar.!\nCobalah!";
			} else if (triggercounter > 3) {
				closeAnotherDialog ();
				triggercounter = 0;
			}
		}
	}

	public void FairyController(int fairyid){
		//animator.GetComponent<Animator> ();
		triggercounter++;
		Debug.Log ("Fairy COUNTER" + triggercounter);
		go.SetActive (false);
		if (triggercounter == 1) {
			PlayerPrefs.SetInt ("fairyid", fairyid);
			Animation anim = GameObject.Find ("Elf" + PlayerPrefs.GetInt ("fairyid")).GetComponentInChildren<Animation> ();
			//animator.SetBool ("Pick", true);
			if (fairyid == 1) {
				//Animation anim = GameObject.Find("Elf"+fairyid).GetComponentInChildren<Animation>();
				name.text = "<color=blue>Tzuyu</color>";
				conversation.text = "Haaaaaaiiii There!! Namaku <color=blue>Tzuyu</color>. Aku adalah peri yang siap membantumu belajar.\n Yuk kita mulai!";
				GameObject.Find ("Elf" + PlayerPrefs.GetInt ("fairyid")).transform.position = new Vector3 (0f, 4f, 0f);
				anotherdialog.transform.position = new Vector3 (20f, 7f, 0f);
				anotherdialog.transform.parent = GameObject.Find ("Elf" + PlayerPrefs.GetInt ("fairyid")).transform;
			} else if (fairyid == 2) {
				//Animation anim = GameObject.Find("Elf"+fairyid).GetComponentInChildren<Animation>();
				name.text = "<color=yellow>Sana</color>";
				conversation.text = "Haaaaaaiiii There!! Namaku <color=yellow>Sana</color>. Aku adalah peri yang siap membantumu belajar.\n Yuk kita mulai!";
				GameObject.Find ("Elf" + PlayerPrefs.GetInt ("fairyid")).transform.position = new Vector3 (0f, 0f, 0f);
				anotherdialog.transform.position = new Vector3 (20f, 7f, 0f);
				anotherdialog.transform.parent = GameObject.Find ("Elf" + PlayerPrefs.GetInt ("fairyid")).transform;
			} else if (fairyid == 3) {
				//Animation anim = GameObject.Find("Elf"+fairyid).GetComponentInChildren<Animation>();
				name.text = "<color=magenta>Mina</color>";
				conversation.text = "Haaaaaaiiii There!! Namaku <color=magenta>Mina</color>. Aku adalah peri yang siap membantumu belajar.\n Yuk kita mulai!";
				GameObject.Find ("Elf" + PlayerPrefs.GetInt ("fairyid")).transform.position = new Vector3 (0f, 0f, 0f);
				anotherdialog.transform.position = new Vector3 (20f, 7f, 0f);
				anotherdialog.transform.parent = GameObject.Find ("Elf" + PlayerPrefs.GetInt ("fairyid")).transform;
			} else {
				Debug.Log ("MASUK KE DIALOG");
			}
			anim.PlayQueued ("skill", QueueMode.PlayNow);
			anotherdialog.SetActive (true);
		} else if (triggercounter == 2) {
			Animation anim = GameObject.Find ("Elf" + PlayerPrefs.GetInt ("fairyid")).GetComponentInChildren<Animation> ();
			conversation.text = "Coba kamu pilih materi pelajarannya terlebih dahulu!";
			anim.PlayQueued ("attack", QueueMode.PlayNow);
		} else if (triggercounter == 3) {
			canvas.SetActive (true);
		}
		else{
			Animation anim = GameObject.Find("Elf"+PlayerPrefs.GetInt("fairyid")).GetComponentInChildren<Animation>();
			anim.PlayQueued ("idle", QueueMode.PlayNow);
			closeAnotherDialog ();
			triggercounter = 0;
		}
	}
	public void BookshelfDialog(GameObject bookshelf){
		//triggercounter = 0;
		go.transform.position = bookshelf.transform.position+new Vector3(0,0,-10);
		go.transform.parent = bookshelf.transform;
		triggercounter++;
		Debug.Log ("BOOKSHELF COUNTER" +triggercounter);
		if (triggercounter == 1) {
			thinking.text = "Sepertinya hanya rak buku biasa...";
			go.SetActive (true);
		} else if (triggercounter > 1) {
			go.SetActive (false);
			triggercounter = 0;
		}
	}

	public void Drawer(GameObject drawer){
		go.transform.position = drawer.transform.position+new Vector3(10,10,0);
		go.transform.parent = drawer.transform;
		triggercounter++;
		Debug.Log ("DRAWER COUNTER" + triggercounter);
		if (triggercounter == 1) {
			thinking.text = "Mungkin ini hanya loker tua biasa...";
			go.SetActive (true);
		} else if (triggercounter > 1) {
			go.SetActive (false);
			triggercounter = 0;
		}
	}

	public void Sofa(GameObject sofa){
		go.transform.position = sofa.transform.position+new Vector3(0,20,0);
		go.transform.parent = sofa.transform;
		triggercounter++;
		Debug.Log ("Sofa COUNTER" + triggercounter);
		if (triggercounter == 1) {
			thinking.text = "Sofa ini terlihat nyaman untuk beristirahat... \nAku mungkin langsung tertidur jika aku duduk disitu..";
			go.SetActive (true);
		} else if (triggercounter > 1) {
			go.SetActive (false);
			triggercounter = 0;
		}
	}

	public void GroupTable(GameObject table){
		go.transform.position = table.transform.position+new Vector3(0,20,0);
		go.transform.parent = table.transform;
		triggercounter++;
		Debug.Log ("Group Table COUNTER" + triggercounter);
		if (triggercounter == 1) {
			thinking.text = "Meja diskusi ini pasti digunakan untuk belajar bersama oleh para pelajar.";
			go.SetActive (true);
		} else if (triggercounter > 1) {
			go.SetActive (false);
			triggercounter = 0;
		}
	}

	public void SingleTable(GameObject table){
		go.transform.position = table.transform.position+new Vector3(0,20,10);
		go.transform.parent = table.transform;
		triggercounter++;
		Debug.Log ("DRAWER COUNTER" + triggercounter);
		if (triggercounter == 1) {
			thinking.text = "Wah belajar di meja ini sepertinya sangat nyaman.";
			go.SetActive (true);
		} else if (triggercounter > 1) {
			go.SetActive (false);
			triggercounter = 0;
		}
	}

	public void Map(GameObject map){
		go.transform.position = map.transform.position+new Vector3(0,0,-10);
		go.transform.parent = map.transform;
		triggercounter++;
		Debug.Log ("MAP COUNTER" + triggercounter);
		if (triggercounter == 1) {
			thinking.text = "WOW.. Peta Dunia Para Peri yang sangat besar!";
			go.SetActive (true);
		} else if (triggercounter > 1) {
			go.SetActive (false);
			triggercounter = 0;
		}
	}

	public void MaidChoice(int i){
		if (i == 1) {
			Application.LoadLevel ("MateriScene");
			conversation.text = "Selamat belajar!";
		} else if (i == 2) {
			Application.LoadLevel ("SciFi Level");
			conversation.text = "Sampaikan salamku ke Capt Roger!";
		} else if (i == 3) {
			conversation.text = "Bye!!";
			Application.Quit ();
		}
	}

	public void CaptChoice(int i){
		if (i == 1) {
			Application.LoadLevel ("MateriScene");
			conversation.text = "Selamat belajar!";
		} else if (i == 2) {
			Application.LoadLevel ("MainScene");
			conversation.text = "Sampaikan salamku ke Eliza!";
		} else if (i == 3) {
			conversation.text = "Bye!!";
			Application.Quit ();
		}
	}
	public void Howto(Text text){
		text.fontSize = 18;
		text.text = "- Gerakan kepalamu dan arahkan pandanganmu kemanapun kamu mau untuk melihat sekitar\n- Gunakan magnet atau tekan layar handphonemu, jangan dilepas untuk berjalan\n- Arahkan pandanganmu ke benda hingga pointer membesar dan gunakan magnet untuk berinteraksi dengan benda/karakter\n- Awasi langkahmu jangan sampai jatuh, bermainlah di tempat yang aman";
	}

	public void triggercount(){
		triggercounter++;
	}
	public void show(GameObject gameobject){
		gameobject.SetActive (true);
	}

	public void hide(GameObject gameobject){
		gameobject.SetActive (false);
	}
	void closeAnotherDialog () {
		anotherdialog.SetActive (false);
	}

}
