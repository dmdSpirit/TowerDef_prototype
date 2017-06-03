using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game controller. Handles units list.
/// </summary>

// TODO: Remake into singleton
public class GameController : MonoBehaviour {
	static public List<GameObject> unitsList;

	List<Spawn> spawnersList;

	void Start(){
		unitsList = new List<GameObject> ();
		spawnersList = new List<Spawn> ();

		GameObject[] spawners = GameObject.FindGameObjectsWithTag ("Spawner");
		foreach(GameObject spawner in spawners){
			Spawn newSpawn = spawner.GetComponent<Spawn> ();
			if (newSpawn != null) {
				spawnersList.Add (newSpawn);
				newSpawn.OnSpawn += OnUnitSpawn;
			}
		}

		if(spawnersList.Count == 0)
			Debug.LogWarning("GameController :: Start - not spawners found.");
	}

	void OnUnitSpawn(GameObject spawnedUnitGO){
		Health spawnedUnitHealth = spawnedUnitGO.GetComponent<Health> ();

		// Every unit must have Health script attached
		if(spawnedUnitHealth != null){ 
			unitsList.Add (spawnedUnitGO);
			spawnedUnitHealth.OnDeath += OnUnitDeath;
		}
		else{
			Debug.LogError ("Spawned unit " + spawnedUnitGO.name + " does not have Heath script attached!");
		}
	}

	void OnUnitDeath(GameObject deadUnitGO){
		unitsList.Remove (deadUnitGO);
	}
}
