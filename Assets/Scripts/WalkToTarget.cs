using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Walking to the target, using NavMesh.
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
public class WalkToTarget : MonoBehaviour {
	GameObject finish;
	NavMeshAgent nmAgent;

	void Start(){
		nmAgent = GetComponent<NavMeshAgent> ();
		finish = GameObject.FindGameObjectWithTag ("Finish");
		if(finish != null)
			nmAgent.SetDestination (finish.transform.position);
	}

	void ChangeTarget(Transform tTransform){
		if(tTransform != null)
			nmAgent.SetDestination (tTransform.position);
	}
}
