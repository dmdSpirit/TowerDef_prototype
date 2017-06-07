﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mouse controller. Handle mouse click.
/// </summary>

public class MouseController : MonoBehaviour {
	public LayerMask isClickable;

	void Update () {
		if(Input.GetButtonDown("Fire1")){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit rayHit;
			if(Physics.Raycast(ray, out rayHit, 100, isClickable)){
				// Debug.Log ("Mouse clicked on " + rayHit.collider.gameObject.name);
				TowerController towerClicked = rayHit.collider.gameObject.GetComponent<TowerController>();
				if (towerClicked != null)
					towerClicked.OnClick ();
			}
		}
	}
}
