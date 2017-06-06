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
		foreach(Transform t in TowerModel.transform){
			MeshRenderer mr = t.GetComponent<MeshRenderer> ();
			if(mr != null)
				b.Encapsulate(mr.bounds);
		}
		BoxCollider bc = GetComponent<BoxCollider> ();
		bc.size = b.size;
		bc.center = b.center - transform.position;
	}
}
