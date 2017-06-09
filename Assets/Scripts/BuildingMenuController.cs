using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuController : MonoBehaviour {
	TowerController selectedTowerController;
	GameObject buildingMenu;

	bool ShowMenu {
		get{
			return _showMenu;
		}
		set{
			_showMenu = value;
			if (buildingMenu == null)
				Debug.LogError (gameObject.name + " :: ShowMenu changed - Building Menu is missing");
			else
				buildingMenu.SetActive (_showMenu);
		}
	}
	bool _showMenu;

	void Start(){
		Transform buildingMenuTransform = transform.Find("Building Menu");
		if(buildingMenuTransform == null)
			Debug.LogError(gameObject.name +" :: Start - Could not find Building Menu child");
		else{
			buildingMenu = buildingMenuTransform.gameObject;
		}
	}

	// FIXME: Hide Building Menu when no towers seleted.
	public void OnTowerSelected(TowerController towerSelected){
		if(buildingMenu == null)
			Debug.LogError(gameObject.name +" :: OnTowerSelected - Building Menu is missing");
		selectedTowerController = towerSelected;
		ShowMenu = true;
		// Reposition Building Menu to the center of selected tower.
		Vector2 newBuildMenuPosition;
		newBuildMenuPosition = Camera.main.WorldToViewportPoint(towerSelected.GetTowerCenter());
		Debug.Log ("OnTowerSelected :: Selected Tower Screen Position: " + newBuildMenuPosition);
		buildingMenu.GetComponent<RectTransform> ().anchoredPosition = newBuildMenuPosition;
		//buildingMenu.transform.position = new Vector3(newBuildMenuPosition.x, newBuildMenuPosition.y, buildingMenu.transform.position.z);
	}


}
