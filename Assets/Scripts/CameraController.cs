using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public bool borderPan = false;
	public float panSpeed = 20f;
	public float panBorderThikness = 15f;
	public float scrollSpeed = 200f;
	// TODO: Add visual representation of camera movement borders.
	public Rect panLimit;
	public Vector2 scrollLimit;
	

	void Update () {
		Vector3 cameraMovemet = new Vector3();
		if (Input.GetKey ("w") || (Input.mousePosition.y >= Screen.height - panBorderThikness && borderPan))
			cameraMovemet.z += 1;
		if (Input.GetKey ("s") || (Input.mousePosition.y <= panBorderThikness && borderPan))
			cameraMovemet.z -= 1;
		if (Input.GetKey ("a") || (Input.mousePosition.x < panBorderThikness && borderPan))
			cameraMovemet.x -= 1;
		if (Input.GetKey ("d") || (Input.mousePosition.x >= Screen.width - panBorderThikness && borderPan))
			cameraMovemet.x += 1;
		cameraMovemet.Normalize ();
		cameraMovemet *= panSpeed * Time.deltaTime;
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		cameraMovemet.y = -scroll * scrollSpeed * Time.deltaTime;

		Vector3 cameraPosition = transform.position+cameraMovemet;
		cameraPosition.x = Mathf.Clamp (cameraPosition.x, panLimit.x, panLimit.width);
		cameraPosition.z = Mathf.Clamp (cameraPosition.z, panLimit.y, panLimit.height);
		cameraPosition.y = Mathf.Clamp (cameraPosition.y, scrollLimit.x, scrollLimit.y);
		transform.position = cameraPosition;
	}
}
