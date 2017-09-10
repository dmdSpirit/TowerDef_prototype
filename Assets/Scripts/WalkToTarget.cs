using UnityEngine;
using UnityEngine.AI;
using System;

/// <summary>
/// Walking to the target, using NavMesh. If the game is paused, walking also stops.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class WalkToTarget : MonoBehaviour {

	public event Action<GameObject> onFinish;
	public float finishRange = 0.1f;

	bool _isMoving;
	GameObject lastDestinationGO;
	Vector3 oldVelocity;
	NavMeshAgent nmAgent;

	public bool IsMoving {
		get{
			return _isMoving;
		}
		set{
			if (_isMoving != value) {
				_isMoving = value;
				ChangeTarget (value ? lastDestinationGO : null);
			}
		}
	}

	void Start(){
		nmAgent = GetComponent<NavMeshAgent> ();
		InitializeMovement ();
	}

	void Update(){
		// Stop moving if the game is paused.
		// FIXME: Remove isMoving check from WalkToTarget.
		//IsMoving = GameController.Instance.GameIsRunning;

		// Fire finish event if object reached its lastDestinationGO, is walking and lastDestinationGO != null.
		if (IsMoving && lastDestinationGO != null &&
		    Vector3.Distance (transform.position, lastDestinationGO.transform.position) <= finishRange
		) {
			if (onFinish != null)
				onFinish (gameObject);
		}
		if(IsMoving == false)
			nmAgent.velocity = new Vector3();
	}

	void InitializeMovement(){
		lastDestinationGO = GameObject.FindGameObjectWithTag ("Finish");
		// Check there is only one object with finish tag.
		if (GameObject.FindGameObjectsWithTag ("Finish").Length > 1)
			Debug.LogError ("[WalkToTarget] There is more than 1 object with tag 'Finish' in scene.");
		if (lastDestinationGO == null)
			Debug.LogError ("[WalkToTarget] No object tagged 'Finish' found.");
		else
			IsMoving = true;
		oldVelocity = new Vector3 ();
		ChangeTarget (lastDestinationGO);
	}

	// If destinationGO is not null, then set it as the new destination, else stop moving.
	void ChangeTarget(GameObject destinationGO){
		if (destinationGO != null) {
			this.lastDestinationGO = destinationGO;
			// Regain velocity after pause.
			nmAgent.velocity = oldVelocity;
			nmAgent.SetDestination (destinationGO.transform.position);
		}
		else{
			oldVelocity = nmAgent.velocity;
			nmAgent.SetDestination (transform.position);
			nmAgent.velocity = new Vector3();
			GetComponent<Rigidbody> ().velocity = new Vector3 ();
		}
	}
}
