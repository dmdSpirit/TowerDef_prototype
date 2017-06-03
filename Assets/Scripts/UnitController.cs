using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unit death animation, unit behavior
/// </summary>

[RequireComponent(typeof(Health))]
public class UnitController : MonoBehaviour {
	void Start(){
		Health unitHealth = GetComponent<Health> ();
		unitHealth.OnDeath += OnUnitDeath;
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
