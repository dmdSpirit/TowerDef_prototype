using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModelController : MonoBehaviour {
	public GameObject TowerModel{
		get { return towerModel;}
		set {
			if (towerModel != value) {
				towerModel = value;
				ChangeColliderSize ();

			}
		}
	}

	GameObject towerModel;
	int towerLevel;


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

	// FIXME: Change collider size
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
		Debug.Log ("Bounds center: " + b.center);
		Debug.Log ("Bounds size: " + b.size);
		Debug.Log ("transform center: " + transform.position);
		bc.size = b.size;
		bc.center = b.center - transform.position;
	}
}
