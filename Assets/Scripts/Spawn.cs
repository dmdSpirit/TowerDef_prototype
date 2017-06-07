using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Unit spawning.
/// </summary>

public class Spawn : MonoBehaviour {
	// TODO: Spawn enemies from a list.
	public GameObject enemyPrefab;
	public float spawnTime = 3;

	public event Action<GameObject> OnSpawn;

	float timePassed;

	void Start(){
		timePassed = 0;
	}

	void Update(){
		timePassed = timePassed + Time.deltaTime;
		if( timePassed >= spawnTime){ // Spawn enemy
			GameObject go = Instantiate( enemyPrefab, transform);
			if(OnSpawn != null)
				OnSpawn(go);
			timePassed = 0;
		}
	}
}
