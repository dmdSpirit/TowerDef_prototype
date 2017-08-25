using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game controller. Handles units list.
/// </summary>
public class GameController : MonoSingleton<GameController> {
	// GameController handles now:
	//					Events from units, spawners, towers
	//					Selected Tower
	//					OnTowerClicked event

	// GameController should handle:
	//					Game state
	//					Events from units, spawners, towers

	private bool _gameIsRunning = true;

	public bool GameIsRunning{
		get {
			return _gameIsRunning;
		}
		set{ 
			if(_gameIsRunning != value){
				// TODO: Register call to 'Game Paused' Event. Stop gameplay and show menu if _gameIsRunning == false.
				_gameIsRunning = value;
				mainMenu.SetActive (!value);
			}
		}
	}

	public GameObject mainMenu;

	void Start(){
		CheckIsSingleInScene ();

		OldStart ();

		// Check that everything is assigned so I won't have to check it every time.
		if (mainMenu == null)
			Debug.LogError ("[GameController] MainMenu is not assigned.");

		// Start game in Paused condition.
		GameIsRunning = false;
	}

	//-	------------- OLD
	static public List<GameObject> unitsList;

	List<Spawn> spawnersList;
	List<TowerController> towersList;

	TowerController selectedTower;
	BuildingMenuController buildingMenuController;
	MouseController mouseController;

	// FIXME: Becoming way too big, refactor.
	void OldStart(){

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
			Debug.LogWarning("GameController :: Start - No spawners found.");

		//Get towers list
		GameObject[] towers = GameObject.FindGameObjectsWithTag ("Tower");
		foreach(GameObject tower in towers){
			TowerController newTower = tower.GetComponent<TowerController> ();
			if (newTower != null) {
				towersList.Add (newTower);
				newTower.onTowerClicked += OnTowerClicked;
			}
		}
		if(spawnersList.Count == 0)
			Debug.LogWarning("GameController :: Start - No towers found.");

		// Get buildingMenuController
		// TODO: Make buildingMenuController singleton.
		GameObject buildingMenuGO = BuildingMenuController.Instance.gameObject;
		if(buildingMenuGO == null)
			Debug.LogError(gameObject.name+" :: Start - Could not find Building Menu");
		else{
			buildingMenuController = buildingMenuGO.GetComponent<BuildingMenuController> ();
			if (buildingMenuController == null)
				Debug.LogError (gameObject.name + " :: Start - "+buildingMenuGO.name+" does not have BuildingMenuController component attached");
			else
				foreach (TowerController towerController in towersList)
					towerController.onTowerSelected += buildingMenuController.OnTowerSelected;
		}

		// Get mouseController
		mouseController = FindObjectOfType<MouseController>();
		if(mouseController == null){
			Debug.LogError(gameObject.name+" :: Start - Could not find Mouse Controller component.");
		}
		else{
			mouseController.unselectEverything += buildingMenuController.HideMenu;
		}
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
