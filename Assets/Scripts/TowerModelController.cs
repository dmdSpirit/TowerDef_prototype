using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Handles tower model as child object of parent Tower.
/// </summary>

public class TowerModelController : MonoBehaviour {
	public GameObject TowerModel{
		get { return _towerModel;}
		set {
			if (_towerModel != value) {
				_towerModel = value;
				ChangeColliderSize ();
				// TODO: Parse level from model name
				if (onModelChanged != null)
					onModelChanged ();
			}
		}
	}
	[SerializeField] //FIXME: For testing.
	GameObject _towerModel;

	public Vector3 towerCenter { get; protected set;}
	public int towerLevel { get; protected set;}
	public event Action onModelChanged;

	// FIXME: ONLY FOR TESTING.
	public GameObject t_towerModel0;
	public GameObject t_towerModel1;

	void Start(){
		Transform modelTransform = transform.GetChild (0);
		if (modelTransform == null) {
			Debug.LogError (gameObject.name + " :: Start - Child object not found.");
			return;
		}
		if( modelTransform.name.StartsWith("Tower") == false){
			Debug.LogError (gameObject.name + " :: Start - Child object is not a tower model.");
			return;
		}
		towerLevel = int.Parse (modelTransform.name.Substring (6));
		TowerModel = modelTransform.gameObject;
		towerCenter = new Vector3 ();
	}

	// FIXME: Refactor me.
	// FIXME: Fix collider size when object is rotated.
	// FIXME: Bounds not doing good. May be depent on the fact it is alway box collider and just code the calculation.
	void ChangeColliderSize(){
		Bounds b = new Bounds ();
		bool hasBounds = false;
		foreach(Transform t in TowerModel.transform){
			MeshRenderer mr = t.GetComponent<MeshRenderer> ();
			if (mr != null) {
				if(hasBounds)
					b.Encapsulate (mr.bounds);
				else{
					b = mr.bounds;
					hasBounds = true;
				}
			}
		}
		BoxCollider bc = GetComponent<BoxCollider> ();
		bc.size = b.size;
		bc.center = b.center - transform.position;
		towerCenter = bc.center;
		//Debug.Log (gameObject.name + " :: ChangeColliderSize - Tower Center = " + towerCenter);
	}

	// TODO: Add ChangeModel.
	public void BuildTowerToLevel(int level){
		if (towerLevel == level)
			return;
		GameObject newTowerModelPrefab;
		String newTowerModelName = "Tower ";
		if(level == 1){
			newTowerModelPrefab = t_towerModel1;
		}
		else{
			newTowerModelPrefab = t_towerModel0;
		}
		Destroy (transform.Find(newTowerModelName+towerLevel).gameObject);
		towerLevel = level;
		TowerModel = Instantiate (newTowerModelPrefab, transform);
		TowerModel.name = newTowerModelName+towerLevel;
	}

	/*public void t_ChangeModel(){
		if (towerLevel == 0) {
			towerLevel = 1;
			Destroy (transform.GetChild (0).gameObject);
			TowerModel = Instantiate (t_towerModel1, transform);
			TowerModel.name = "Tower 1";

		} else {
			towerLevel = 0;
			Destroy (transform.GetChild (0).gameObject);
			TowerModel = Instantiate (t_towerModel0, transform);
			TowerModel.name = "Tower 0";

		}
	}*/
}
