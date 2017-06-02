using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shooting bullets towards the target, handling shooting cd.
/// </summary>

public class Shoot : MonoBehaviour {
	public GameObject target; // FIXME: Get the target from controlling script.
	public GameObject bulletPrefab; // FIXME: Set by controlling script.
	public float shootingCD = 1f;
	public float damage = 1f;

	float timePassed;

	void Start(){
		timePassed = 0;
	}

	void Update(){
		timePassed += Time.deltaTime;
		if(timePassed >= shootingCD){
			//Debug.Log (gameObject.name + " :: Shoot");
			timePassed = 0;
		}
	}
}
