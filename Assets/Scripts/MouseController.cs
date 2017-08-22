using UnityEngine;
using System;

/// <summary>
/// Mouse controller. Handle mouse clicking and zooming.
/// </summary>
public class MouseController : MonoSingleton<MouseController> {

	void Start(){
		CheckIsSingleInScene ();
	}

	void Update(){
		// Camera Zoom.
		// FIXME: Deal with twitchy scroll.
		CameraController.Instance.AddToCameraMovement (
			new Vector3 (0, -Input.GetAxis ("Mouse ScrollWheel"), 0)
		);

		OldUpdate ();
	}

	//----- OLD


	public LayerMask isClickable;

	public event Action unselectEverything;

	void OldUpdate () {

		if(Input.GetButtonDown("Fire1")){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit rayHit;
			if(Physics.Raycast(ray, out rayHit, 100, isClickable)){
				// Debug.Log ("Mouse clicked on " + rayHit.collider.gameObject.name);
				TowerController towerClicked = rayHit.collider.gameObject.GetComponent<TowerController>();
				if (towerClicked != null)
					towerClicked.OnClick ();
			}
		}
		if(Input.GetButtonDown("Fire2")){
			if (unselectEverything != null)
				unselectEverything ();
		}
	}
}
