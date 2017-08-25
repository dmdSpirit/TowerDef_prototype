using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuController : MonoSingleton<BuildingMenuController> {
	

	//--------------------------- old
	TowerController selectedTowerController;
	GameObject buildingMenu;
	Dictionary<Button,int> buildButtonsList;

	// FIXME: Remove hardcoded coordinates.
	//public Vector3 defaultPosition;

	// TODO: Unselect Tower event.

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
		CheckIsSingleInScene ();
		buildButtonsList = new Dictionary<Button, int> ();
		//defaultPosition = new Vector3 (-3, 2.7f, -3);
		Transform buildingMenuTransform = transform;
		if(buildingMenuTransform == null)
			Debug.LogError(gameObject.name +" :: Start - Could not find Building Menu child");
		else{
			buildingMenu = buildingMenuTransform.gameObject;
			int num = 0;
			foreach(Transform buttonTranfsorm in buildingMenu.transform){
				Button button = buttonTranfsorm.GetComponent<Button> ();
				if(button != null){
					buildButtonsList.Add (button, num);
					num++;
				}
			}
		}
		ShowMenu = false;
	}

	// FIXME: Hide Building Menu when no towers seleted.
	public void OnTowerSelected(TowerController towerSelected){
		if(buildingMenu == null)
			Debug.LogError(gameObject.name +" :: OnTowerSelected - Building Menu is missing");
		selectedTowerController = towerSelected;
		ShowMenu = true;

		// Reposition Building Menu to the center of selected tower.
		//transform.SetParent(towerSelected.transform, false);
		//transform.localPosition = defaultPosition;
		//transform.localPosition += towerSelected.GetTowerCenter ();

		// Reassign Buttons onClick.
		foreach(KeyValuePair<Button, int> button in buildButtonsList){
			button.Key.onClick.RemoveAllListeners ();
			button.Key.onClick.AddListener (() => selectedTowerController.BuildTower (button.Value));
			//button.Key.onClick.AddListener (HideMenu);
		}
	}

	// When tower gets unselected, hide menu.
	public void HideMenu(){
		ShowMenu = false;
		foreach (KeyValuePair<Button, int> button in buildButtonsList)
			button.Key.onClick.RemoveAllListeners ();
		//transform.SetParent (null);
		//transform.position = defaultPosition;
	}
}
