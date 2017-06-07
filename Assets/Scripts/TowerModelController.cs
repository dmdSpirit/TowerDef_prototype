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
				if (onModelChanged != null)
					onModelChanged ();
			}
		}
	}
	GameObject _towerModel;

	public int towerLevel { get; protected set;}
	public event Action onModelChanged;


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
	}

	// FIXME: Refactor me.
	// FIXME: Fix collider size when object is rotated.
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
	}

	// TODO: Add ChangeModel.
}
