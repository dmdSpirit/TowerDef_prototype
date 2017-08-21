using UnityEngine;

/// <summary>
/// Move Camera using WASD keys, zoom with scroll wheel.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraController : MonoSingleton<CameraController> {
	// TODO: Camera movement boundaries.
	// TODO: Moving Camera using mouse.

	[SerializeField]
	float cameraSpeed = 20f;
	[SerializeField]
	float scrollSpeed = 500f;
	[SerializeField]
	Vector2 scrollLimit = new Vector2(10, 50);

	protected CameraController(){}

	void Start(){
		CameraController test = Instance; // Test that there is only one CameraController component in scene.
	}

	void Update(){
		var cameraMovement = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		cameraMovement.Normalize ();
		cameraMovement *= cameraSpeed; // Y component is sill 0, so we can multiply.
		cameraMovement.y = - Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
		Vector3 newCameraPosition = transform.position + cameraMovement * Time.deltaTime;
		newCameraPosition.y = Mathf.Clamp (newCameraPosition.y, scrollLimit.x, scrollLimit.y);
		transform.position = newCameraPosition;
	}
}