﻿using System.Collections;
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
	public event Action<TowerController> onTowerSelected;

	public bool IsSelected {
		get { return _isSelected;}
		set { 
			_isSelected = value;
			if (rangeProjector != null)
				rangeProjector.enabled = value;
			if (_isSelected && onTowerSelected != null)
				onTowerSelected (this);
		}
	}
	bool _isSelected = false;

	public Transform shootingBase;
	public GameObject target;
	Projector rangeProjector;
	TowerModelController towerModelController;


	void Start(){
		towerModelController = GetComponent<TowerModelController> ();
		if(towerModelController == null){
			Debug.LogError (gameObject.name + " :: Start  - towerModelController is missing.");
			return;
		}
		towerModelController.onModelChanged += OnModelChanged;
		OnModelChanged ();
	}

	void Update(){
		if(target == null || target.GetComponent<UnitController>().isAlive == false){
			target = GetTarget();
		}
		else{
			float newDistance = Vector3.Distance (transform.position, target.transform.position);
			if(newDistance > shootingRange)
				target = GetTarget();
		}

		if (target != null && shootingBase != null){
			Transform unitTarget = target.GetComponent<UnitController> ().shootingTarget;
			//Debug.DrawLine (shootingBase.position, unitTarget.position,Color.red);

		}
	}

	GameObject GetTarget(){
		float distance = Mathf.Infinity;
		GameObject newTarget = null;
		if (GameController.unitsList == null) {
			return null;
		}
		foreach(GameObject enemyUnit in GameController.unitsList){
			float newDistance = Vector3.Distance (transform.position, enemyUnit.transform.position);
			if( newDistance <= distance && enemyUnit.GetComponent<UnitController>().isAlive){
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
		// TODO: Implement selection.
		// TODO: Implement selection shader.
		//Debug.Log (gameObject.name + " :: OnClick");
		if (onTowerClicked != null)
			onTowerClicked (gameObject);
	}

	// Reset rangeProjector transform and shootingBase based on new model.
	void OnModelChanged(){
		if(towerModelController == null){
			Debug.LogError (gameObject.name + " :: OnModelChanged  - towerModelController is missing.");
			return;
		}
		// FIXME: I will assume that 0 lvl tower is base only and does not have Shooting Base or Range Projector.
		if (towerModelController.towerLevel > 0) {
			shootingBase = towerModelController.TowerModel.transform.Find ("Shooting Base");
			if (shootingBase == null)
				Debug.LogError (gameObject.name + " :: OnModelChanged - Shooting Base child not found.");

			Transform rangeProjectorTransform = towerModelController.TowerModel.transform.Find ("Range Projector");
			if (rangeProjectorTransform == null)
				Debug.LogError (gameObject.name + " :: OnModelChanged - Range Projector child not found.");
			else {
				Projector rngProjector = rangeProjectorTransform.GetComponent<Projector> ();
				if (rngProjector == null) {
					Debug.LogError (gameObject.name + " :: OnModelChanged - Range Projector does not have Projector component.");
				} else {
					rangeProjector = rngProjector;
				}
			}
		}
		else if(towerModelController.towerLevel ==0){
			shootingBase = null;
			rangeProjector = null;
		}
	}

	public Vector3 GetTowerCenter(){
		Vector3 towerCenter = new Vector3 ();
		if(towerModelController == null){
			Debug.LogError(gameObject.name + " :: GetTowerCenter - towerModelController is missing");
			return towerCenter;
		}
		return towerModelController.towerCenter;
	}

	// Wrap Tower Model Controller function .
	public void BuildTower(int level){
		towerModelController.BuildTowerToLevel(level);
	}
}
