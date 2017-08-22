using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Move Camera using WASD keys, zoom with scroll wheel.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraController : MonoSingleton<CameraController> {
	// TODO: Camera movement boundaries.
	// TODO: Moving Camera using mouse.
	// TODO: Zoom towards mouse pointer.

	[SerializeField]
	float cameraSpeed = 20f;
	[SerializeField]
	float scrollSpeed = 500f;
	[SerializeField]
	Vector2 scrollLimit = new Vector2(10, 50);

	List<Vector3> cameraMovementList;

	void Start(){
		CheckIsSingleInScene ();

		cameraMovementList = new List<Vector3> ();
	}

	void Update(){
		MoveCamera ();
	}

	void MoveCamera(){
		Vector3 newCameraPosition = transform.position + GetFinalMovement() * Time.deltaTime;
		newCameraPosition.y = Mathf.Clamp (newCameraPosition.y, scrollLimit.x, scrollLimit.y);
		transform.position = newCameraPosition;
	}

	// Store all movement from every source till the next Update call.
	public void AddToCameraMovement(Vector3 movement){
		// Normalize incoming vectors. Can be change in case of weighted movement controlls. 
		movement.Normalize ();
		cameraMovementList.Add (movement);
	}

	// Add every movement vector up and then normalize (x,0,z) and (0,y,0), so that final scroll and movement
	// have different speed.
	Vector3 GetFinalMovement(){
		Vector3 finalMovement = new Vector3 ();
		// Camera doesn't move if the game is paused.
		if (GameController.Instance.GameIsRunning) {
			foreach (Vector3 movement in cameraMovementList)
				finalMovement += movement;
			Vector3 finalScroll = new Vector3 (0, finalMovement.y, 0);
			finalMovement -= finalScroll;
			finalScroll.y = Mathf.Clamp (finalScroll.y, -1, 1);
			finalMovement.Normalize ();
			finalMovement = finalMovement * cameraSpeed + finalScroll * scrollSpeed;
		}
		cameraMovementList.Clear ();
		return finalMovement;
	}

}