using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/// <summary>
/// Unit death animation, unit behavior.
/// </summary>
[RequireComponent(typeof(Health))]
public class UnitController : MonoBehaviour {


	// ----- OLD

	Animator animator;
	public Transform shootingTarget;
	[SerializeField]
	float deathAnimationLength = 5;
	[SerializeField]
	float corpseHideSpeed = 1f;

	public bool isAlive = true;
	bool hasTorch;

	public GameObject torchPrefab;
	// finger1_R

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

		Random.InitState ((int)System.DateTime.Now.Ticks);
		hasTorch = Random.value >= 0.5;
		if (hasTorch)
			CreateTorch ();
	}

	public void OnUnitDeath(GameObject deadUnitGO){
		if (isAlive == false)
			return;
		if(deadUnitGO != gameObject){
			Debug.LogError(gameObject.name + " :: OnUnitDeath - Got death event from other unit.");
			return;
		}
		// TODO: Add unit death animation.
		//Debug.Log(gameObject.name + " is dead.");
		//Destroy (gameObject);
		StartCoroutine("DeathAnimation");
	}

	void OnFinish(GameObject finishedGameObject){
		if (finishedGameObject != gameObject)
			Debug.LogError (gameObject.name + " :: OnFinish - event from wrong Game Object.");
		OnUnitDeath (finishedGameObject);

		// TODO: Implement damage to the core event.
	}

	IEnumerator DeathAnimation(){
		//GetComponent<Health> ().enabled = false;
		isAlive = false;
		walkToTarget.IsMoving = false;
		GetComponent<NavMeshAgent> ().radius = 0;
		GetComponent<Collider> ().enabled = false;
		animator.SetTrigger ("Death");
		yield return new WaitForSeconds(deathAnimationLength);
		//Destroy (GetComponent<Rigidbody> ());
		//StartCoroutine ("HideCorpse");
		Destroy (gameObject);
	}

	IEnumerator HideCorpse(){
		Vector3 corpseMovement = new Vector3 (0, corpseHideSpeed, 0);
		float timePassed = 0;
		while(timePassed<deathAnimationLength){
			transform.position -= corpseMovement * corpseHideSpeed*Time.deltaTime;
			timePassed += Time.deltaTime;
			yield return null;
		}
		Destroy (gameObject);
	}

	void CreateTorch(){
		Transform wristTransform;
		Vector3 torchPosiiton;
		wristTransform = transform.Find ("finger2_L");
		torchPosiiton = new Vector3 (0.001f, 0.115f, 0.057f);
		GameObject torch = Instantiate (torchPrefab, wristTransform);
		torch.transform.localPosition = torchPosiiton;
	}
}
