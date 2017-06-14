using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	public float horizontal;
	public float vertical;
	public Transform container;
	public float minAngle = -45.0f;
	public float maxAngle = 45.0f;
	float yRotate = 0.0f;
	float xRotate = 0.0f;
	float turnSpeedMouse = 75.0f;
	Transform cameraTransform;

	void LateUpdate () 
	{
		//Using mouse
		horizontal = Input.GetAxis("Mouse X");
		vertical= Input.GetAxis("Mouse Y");

		//This is made in order to avoid rotation on Z, just by typing 0 on Zcoord isn’t enough
		//so the container is rotated around Y and the camera around X separately
		container.Rotate(new Vector3(0, horizontal*(-1), 0f)*Time.deltaTime*turnSpeedMouse);
		transform.Rotate(new Vector3(vertical, 0, 0)*Time.deltaTime*turnSpeedMouse);
		xRotate += Input.GetAxis ("Mouse X") * turnSpeedMouse * Time.deltaTime;
		yRotate += Input.GetAxis ("Mouse Y") * turnSpeedMouse * Time.deltaTime;
		//yRotate = Mathf.Clamp (yRotate, minAngle, maxAngle);
		transform.eulerAngles = new Vector3 (yRotate, xRotate, 0.0f);
	}
}


