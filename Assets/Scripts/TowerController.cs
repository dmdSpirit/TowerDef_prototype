using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Tower controller. Choosing targets.
/// </summary>


// TODO: Control the tower object depending on what tower is actually build. Different components?
public class TowerController : MonoBehaviour {
	// TODO: Implement Range Circle resizing, when shootingRange is changed.
	public float shootingRange = 3f;
	public event Action<GameObject> onTowerClicked;

	public bool IsSelected {
		get { return isSelected;}
		set { 
			isSelected = value;
			rangeProjector.enabled = value;
		}
	}

	bool isSelected = false;

	Transform shootingBase;
	GameObject target;
	Projector rangeProjector;

	public GameObject towerModel;
	public int towerLevel=0;

	void Start(){
		if(towerModel == null){
			Debug.LogError (gameObject.name + " :: Start  - towerModel is not set.");
			return;
		}

		// FIXME: Set child with model by script
		// TODO: Change collider when model is changed
		// FIXME: Separate code handling tower model to TowerModelContoroller

		// FIXME: I will assume that 1 lvl tower is base only and does not have Shooting Base or Range Projector
		if (towerLevel == 0)
			return;

		shootingBase = towerModel.transform.Find("Shooting Base");
		if(shootingBase == null) Debug.LogError(gameObject.name + " :: Start - Shooting Base child not found.");

		Transform rangeProjectorTransform = towerModel.transform.Find("Range Projector");
		if(rangeProjectorTransform == null)
			Debug.LogError(gameObject.name + " :: Start - Range Projector child not found.");
		else{
			Projector rngProjector = rangeProjectorTransform.GetComponent<Projector> ();
			if (rngProjector == null) {
				Debug.LogError (gameObject.name + " :: Start - Range Projector does not have Projector component.");
			} else
				rangeProjector = rngProjector;
		}
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
		if (GameController.unitsList == null) {
			//Debug.LogError (gameObject.name + " :: GetTarget - GameController unitsList not found");
			return null;
		}
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

	public void OnClick(){
		Debug.Log (gameObject.name + " :: OnClick");
		if (onTowerClicked != null)
			onTowerClicked (gameObject);
	}
}
