public class MainMenuController : MonoSingleton<MainMenuController> {
	
	void Start(){
		CheckIsSingleInScene ();
	}

	public void ResumeGame(){
		GameController.Instance.GameIsRunning = true;
	}

	public void Exit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
//		#else
//		Application.Quit();
		#endif
	}
}
