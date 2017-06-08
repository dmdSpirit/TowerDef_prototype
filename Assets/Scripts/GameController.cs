using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game controller. Handles units list.
/// </summary>

// TODO: Remake into singleton.
public class GameController : MonoBehaviour {
	static public List<GameObject> unitsList;

	List<Spawn> spawnersList;
	List<TowerController> towersList;

	TowerController selectedTower;

	void Start(){
		unitsList = new List<GameObject> ();
		spawnersList = new List<Spawn> ();
		towersList = new List<TowerController> ();

		//Get spawners list
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

		//Get towers list
		GameObject[] towers = GameObject.FindGameObjectsWithTag ("Tower");
		foreach(GameObject tower in towers){
			TowerController newTower = tower.GetComponent<TowerController> ();
			if (newTower != null) {
				towersList.Add (newTower);
				// FIXME: For testing.
				//newTower.onTowerClicked += OnTowerClicked;
			}
		}
		if(spawnersList.Count == 0)
			Debug.LogWarning("GameController :: Start - not spawners found.");
	}

	void OnUnitSpawn(GameObject spawnedUnitGO){
		Health spawnedUnitHealth = spawnedUnitGO.GetComponent<Health> ();
		WalkToTarget walkToTarget = spawnedUnitGO.GetComponent<WalkToTarget> ();
		// Every unit must have Health script attached
		if(spawnedUnitHealth != null && walkToTarget != null){ 
			unitsList.Add (spawnedUnitGO);
			spawnedUnitHealth.OnDeath += RemoveUnitFromList;
			walkToTarget.onFinish += RemoveUnitFromList;
		}
		else if(spawnedUnitHealth == null){
			Debug.LogError ("Spawned unit " + spawnedUnitGO.name + " does not have Heath script attached!");
		}
		else if(walkToTarget == null){
			Debug.LogError ("Spawned unit " + spawnedUnitGO.name + " does not have WalkToTarget script attached!");
		}
	}

	void RemoveUnitFromList(GameObject deadUnitGO){
		unitsList.Remove (deadUnitGO);
	}

	void OnTowerClicked(GameObject towerClicked){
		TowerController clickedTowerController = towerClicked.GetComponent<TowerController> ();
		if(clickedTowerController == null)
			Debug.LogError("GameController :: OnTowerClicked - "+towerClicked.name+" does not have TowerController script.");
		else{
			if (selectedTower != null)
				selectedTower.IsSelected = false;
			selectedTower = clickedTowerController;
			selectedTower.IsSelected = true;
		}
	}
}
