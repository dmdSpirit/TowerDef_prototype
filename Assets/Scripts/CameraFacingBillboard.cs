using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour {
	new Camera camera;
	[SerializeField]
	Vector3 billboardPosition = new Vector3(0, 0.5f, -1);

	void Start () {
		camera = Camera.main;
	}

	void Update () {
		transform.position = transform.parent.position + billboardPosition;
		transform.LookAt (transform.position + camera.transform.rotation * Vector3.forward,
			camera.transform.rotation * Vector3.up);
	}
}
