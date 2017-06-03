using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower controller. Choosing targets.
/// </summary>

public class TowerController : MonoBehaviour {
	public GameObject target;
	public float shootingRange = 3f;

	Transform shootingBase;

	void Start(){
		shootingBase = transform.Find("Shooting Base");
		if(shootingBase == null) Debug.LogError(gameObject.name + " :: Start - Shooting base child not found.");
	}

	void Update(){
		if(target == null){
			target = GetTarget();
		}
		else{
			float newDistance = Vector3.Distance (transform.position, target.transform.position);
			if(newDistance > shootingRange)
				target = GetTarget();
		}

		if (target != null){
			Transform unitTarget = target.GetComponent<UnitController> ().shootingTarget;
			Debug.DrawLine (shootingBase.position, unitTarget.position,Color.red);

		}
	}

	GameObject GetTarget(){
		float distance = Mathf.Infinity;
		GameObject newTarget = null;
		foreach(GameObject enemyUnit in GameController.unitsList){
			float newDistance = Vector3.Distance (transform.position, enemyUnit.transform.position);
			if( newDistance <= distance){
				newTarget = enemyUnit;
				distance = newDistance;
			}
		}
		if (distance <= shootingRange)
			return newTarget;
		else
			return null;
	}
}
