using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Tower part. Handles clicking on model.
/// </summary>

public class TowerPart : MonoBehaviour {

	// FIXME: !Do parts really have to track clicking or 1 collider for whole tower can be used?
	public event Action<GameObject> onClicked;

	void Start(){
		//Get parent game object and check it has Tower script
		Transform parentTransform = transform.parent;
		if (parentTransform == null)
			Debug.LogError (gameObject.name + " :: Start - Tower Part could not find parent object");
		else{
			Tower parentTower = parentTransform.gameObject.GetComponent<Tower> ();
			if(parentTower == null)
				Debug.LogError (gameObject.name + " :: Start - Tower Part parent object does not have Tower script");
			else{
				// If everything is ok subscribe parent tower object to onClicked event.
				onClicked += parentTower.OnPartClicked;
			}
		}
	}

	void PartClickedDebug(GameObject partGO){
		Debug.Log (gameObject.name + " was clicked. Input GO: " + partGO);
	}

	// TODO: Create mouse controller script to handle mouse clicking.

}
