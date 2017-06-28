using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLookWalk : MonoBehaviour {

	public Transform vrCamera;

	public float speed = 3.0f;

	public bool moveForward;

	public CharacterController cc;

	public GameObject go;

	public GameObject anothergo;
	//public GameObject go;
	// Use this for initialization
	void Start () {
		cc.GetComponent <CharacterController> ();
		//go.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetButton("Fire1"))&&(!go.activeInHierarchy)&&(!anothergo.activeInHierarchy)) {
			moveForward = true;
			//go.SetActive (false);
		} else {
			moveForward = false;
		}

		if (moveForward) {
			Vector3 forward = vrCamera.TransformDirection (Vector3.forward);
			cc.SimpleMove (forward * speed);
		}
	}

}
