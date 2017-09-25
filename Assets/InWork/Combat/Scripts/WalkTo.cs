using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WalkTo : MonoBehaviour {
	public Transform target;
	NavMeshAgent agent;

	public bool isWalking = true;

	void Start(){
		agent = GetComponent<NavMeshAgent> ();
		if (target != null)
			agent.SetDestination (target.position);
		else
			isWalking = false;
	}
}
