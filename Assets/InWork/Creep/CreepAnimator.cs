using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepAnimator : MonoBehaviour {
	public float maxRadius = 20;
	public float currentRadius = 0;
	public float spreadSpeed = 2;

	void Update () {
		if(currentRadius < maxRadius){
			currentRadius += spreadSpeed * Time.deltaTime;
			currentRadius = Mathf.Min (maxRadius, currentRadius);
			Vector3 newSize = new Vector3 (currentRadius, transform.localScale.y, currentRadius);
			transform.localScale = newSize;
		}
	}
}
