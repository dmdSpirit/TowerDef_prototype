using UnityEngine;

/// <summary>
/// Base class for all MonoBehaviour singleton components.
/// </summary>

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static T _instance;
	private static object _lock = new object();

	private static bool applicationIsQuitting = false;

	public static T Instance{
		get{
			if(applicationIsQuitting){
				Debug.LogWarning ("[Singleton] Instance '" + typeof(T) +
					"' already destroyed on application quit." +
					" Won't create again - returning null.");
				return null;
			}

			lock(_lock){
				if(_instance == null){
					_instance = (T)FindObjectOfType (typeof(T));
					var instanceList = FindObjectsOfType (typeof(T));
					if(instanceList.Length > 1){
						Debug.LogError ("[Singleton] Something went really wrong " +
							" - there should never be more than 1 singleton of type '" +
							typeof(T).ToString() + "'! Reopening the scene might fix it.");
						string objectsList = "";
						char[] toTrim = { ',', ' ' };
						foreach (T inst in instanceList)
							objectsList += string.Format ("{0}, ", inst.gameObject.name);
						objectsList = objectsList.TrimEnd(toTrim);
						Debug.LogError ("[Singleton] List of GameObjects containing '" +
							typeof(T).ToString () + "' component:" + objectsList + ".");
						return _instance;
					}

					if(_instance == null){
						GameObject singleton = new GameObject ();
						_instance = singleton.AddComponent<T> ();
						singleton.name = "(singleton)" + typeof(T).ToString ();

						DontDestroyOnLoad (singleton);

						Debug.Log ("[Singleton] An instance of " + typeof(T) +
							"is needed in scene, so '" + singleton +
							"' was created with DontDestroyOnLoad.");
					}
					else {
						Debug.Log ("[Singleton] Using instance already created: " +
							_instance.gameObject.name);
					}
				}
				return _instance;
			}
		}
	}

	public void OnDestroy(){
		applicationIsQuitting = true;
	}
}
