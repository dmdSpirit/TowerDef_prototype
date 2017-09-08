using UnityEngine;

/// <summary>
/// Unit death animation, unit behavior.
/// </summary>
[RequireComponent(typeof(Health))]
public class UnitController : MonoBehaviour {

	void Update(){
		if (Input.GetKeyDown (KeyCode.J))
			animator.SetTrigger ("Death");
	}

	// ----- OLD

	Animator animator;
	public Transform shootingTarget;

	WalkToTarget walkToTarget;

	void Start(){
		Health unitHealth = GetComponent<Health> ();
		unitHealth.OnDeath += OnUnitDeath;

		shootingTarget = transform.Find ("Shooting Target");
		if(shootingTarget == null)
			Debug.LogError(gameObject.name+" :: Start - Shooting Target not found.");

		// Subscribe to onFinish event.
		walkToTarget = GetComponent<WalkToTarget>();
		if (walkToTarget != null)
			walkToTarget.onFinish += OnFinish;

		animator = GetComponent<Animator> ();
	}

	public void OnUnitDeath(GameObject deadUnitGO){
		if(deadUnitGO != gameObject){
			Debug.LogError(gameObject.name + " :: OnUnitDeath - Got death event from other unit.");
			return;
		}
		// TODO: Add unit death animation.
		//Debug.Log(gameObject.name + " is dead.");
		Destroy (gameObject);
	}

	void OnFinish(GameObject finishedGameObject){
		if (finishedGameObject != gameObject)
			Debug.LogError (gameObject.name + " :: OnFinish - event from wrong Game Object.");
		OnUnitDeath (finishedGameObject);

		// TODO: Implement damage to the core event.
	}
}
