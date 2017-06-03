using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unit death animation, unit behavior
/// </summary>

[RequireComponent(typeof(Health))]
public class UnitController : MonoBehaviour {
	public Transform shootingTarget;

	void Start(){
		Health unitHealth = GetComponent<Health> ();
		unitHealth.OnDeath += OnUnitDeath;

		shootingTarget = transform.Find ("Shooting Target");
		if(shootingTarget == null)
			Debug.LogError(gameObject.name+" :: Start - Shooting Target not found.");
	}

	void OnUnitDeath(GameObject deadUnitGO){
		if(deadUnitGO != gameObject){
			Debug.LogError(gameObject.name + " :: OnUnitDeath - Got death event from other unit.");
			return;
		}
		// TODO: Add unit death animation
		Destroy (gameObject);
	}
}
