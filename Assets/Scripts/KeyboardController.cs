using UnityEngine; 

public class KeyboardController : MonoSingleton<KeyboardController> {

	void Start () {
		CheckIsSingleInScene ();
	}

	void Update(){
		// Pressing 'Esc' pauses/unpauses the game.
		if(Input.GetKeyDown(KeyCode.Escape)){
			GameController.Instance.GameIsRunning = ! GameController.Instance.GameIsRunning;

		}

		// Camera movement.
		CameraController.Instance.AddToCameraMovement (
			new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))
		);
	}
}
