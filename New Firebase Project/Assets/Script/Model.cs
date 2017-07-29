using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

	//public GameObject model;
	// Use this for initialization
	private float sensitivity;
	private Vector3 mouseReference;
	private Vector3 mouseOffset;
	private Vector3 rotation;
	private bool isRotating;
	private int click;
	void Start () {
		//Instantiate (Resources.Load ("Models/IPA/Cover/crystal_17_2"), model.transform.position, Quaternion.identity, model.transform);
		sensitivity = 2.0f;
		rotation = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {
		if (isRotating) {
			//offset
			mouseOffset = (Input.mousePosition - mouseReference);
			//apply rotation
			rotation.y = -(mouseOffset.x) * sensitivity;
			rotation.x = -(mouseOffset.y) * sensitivity;
			//rotate
			transform.eulerAngles += rotation;
			//store mouse
			mouseReference = Input.mousePosition;
		}
	}
	public void onMouseDown()
	{
		isRotating = true;
		mouseReference = Input.mousePosition;
	}

	public void onMouseUp(){
		isRotating = false;
	}

	public void zoomIn(){
		transform.localScale += new Vector3 (0.5f, 0.5f, 0.5f);
		click++;
	}

	public void zoomOut(){
		transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		click--;
	}

	public void backtoNormal(){
		if (click > 0) {
			for (int i = 0; i < click; i++) {
				transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
			}		
		} else if (click < 0) {
			for (int i = 0; i > click; i--) {
				transform.localScale += new Vector3 (0.5f, 0.5f, 0.5f);
			}	
		}
		click = 0;
	}
}
