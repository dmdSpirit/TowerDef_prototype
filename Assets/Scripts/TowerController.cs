using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower controller. Choosing targets.
/// </summary>

public class TowerController : MonoBehaviour {
	public GameObject target;
	public float shootingRange = 3f;

	GameObject GetTarget(){
		// FIXME: Get targets in range from global Dictionary in "Game Controller"
		return null;
	}
}
