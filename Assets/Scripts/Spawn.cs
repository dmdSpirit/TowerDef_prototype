using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
	// TODO: Spawn enemies from a list
	public GameObject enemyPrefab;
	public float spawnTime = 3;

	float timeLeft;

	void Start(){
		timeLeft = spawnTime;
	}

	void Update(){
		timeLeft = Mathf.Max( timeLeft - Time.deltaTime, 0);
		if( timeLeft == 0){ // Spawn enemy
			Instantiate( enemyPrefab, transform);
			timeLeft = spawnTime;
		}
	}
}
