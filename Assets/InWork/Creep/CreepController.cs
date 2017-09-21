using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepController : MonoBehaviour {

	void OnTriggerEnter (Collider collider){
		Debug.Log (collider.name + " is in the creep.");
	}
}
