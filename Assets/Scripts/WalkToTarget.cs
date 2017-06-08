using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

/// <summary>
/// Walking to the target, using NavMesh.
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
public class WalkToTarget : MonoBehaviour {
	public GameObject finish;
	// TODO: Get finish range from finish object.
	public float finishRange = 0.1f;
	public event Action<GameObject> onFinish;

	NavMeshAgent nmAgent;
	UnitController unitController;

	void Start(){
		nmAgent = GetComponent<NavMeshAgent> ();
		finish = GameObject.FindGameObjectWithTag ("Finish");
		if(finish != null)
			nmAgent.SetDestination (finish.transform.position);
		unitController = GetComponent<UnitController> ();
	}

	void ChangeTarget(Transform tTransform){
		if(tTransform != null)
			nmAgent.SetDestination (tTransform.position);
	}

	// Unit fires a finish event on reaching finish point. 
	void Update(){
		if(finish !=null && Vector3.Distance(transform.position, finish.transform.position)<=finishRange)
			if (onFinish != null)
				onFinish (gameObject);
	}
}
