using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkVR : MonoBehaviour {
	public Transform vrCamera;

	public float toogleAngle = 30.0f;

	public float speed = 3.0f;

	public float delay = 0.5f;

	public bool moveForward, moveBackward;

	private CharacterController cc;
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {
		//moveForward = false;
		//moveBackward = false;
		if (Input.GetButton("Fire1"))
			//if (Input.touchCount > 0)
		{
			delay -= Time.deltaTime;
			if (delay < 0)
				moveForward = true;
		}
		/*else if (Input.GetButton("Fire2"))
        {
            moveBackward = true;
        }*/
		/*if (vrCamera.eulerAngles.x >= toogleAngle && vrCamera.eulerAngles.x < 90.0f)
		{
			moveForward = true;
		} */
		else
		{
			//GUI.Box(new Rect(0, 0, Screen.width, Screen.height), this.GetType().Name);
			moveForward = false;
			delay = 0.5f;
			//moveBackward = false;
		}

		if (moveForward)
		{
			//Vector3 forward = cc.transform.TransformDirection(Vector3.forward);

			Vector3 forward = vrCamera.TransformDirection(Vector3.forward);
			cc.SimpleMove(forward * speed);
		}
		/*else if (moveBackward)
        {
            Vector3 forward = vrCamera.TransformDirection(Vector3.back);

            cc.SimpleMove(forward * speed);
        }*/
	}
}
