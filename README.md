## Tower Defence game prototype.
 Just trying things out. Experimenting with game mechanics, ideas and looks.
 
 Goal - finished prototype, that can be added to my portfolio.

### Final Product MVP
- 3 types of enemies
- 3 levels
- 6 towers (2 types + 2 upgrades each)

![Alt text](/Screenshots/video.gif?raw=true "Gameplay")



### Current TODO:
TODO:

	- [x] Fix building menu.
	- [x] Fix tower prefab.
	- [x] Create roads.
	- [x] Fix HumanoidDying animation.
	- [x] Pause running animation.
	- [x] Pause spawn.
	- [x] Add dying coroutine.
	- [x] Create new mesh for the road.
	- [x] Add torches.
	- [ ] Pause mouse controller.
	- [ ] Pause building menu.
	- [ ] Show which tower is selected.
	- [ ] Place towers.
	- [ ] Show HP of units.
	- [ ] Show damage numbers.
	- [ ] Show score.
	- [ ] Create defeat scene.
	- [ ] Show HP of the house.
	- [ ] Create 6 tower models.
	- [ ] Controll building menu with keyboard.
	- [ ] Create 'bullet pool'
	- [ ] Unselect by rightclicking.
	- [ ] Refactor code.
	- [ ] Add forks.
	- [ ] Add trees.

*Code:

	TODO:

		BuildingMenuController
			- [ ] Unselect Tower event.
		CameraController
			- [ ] Camera movement boundaries.
			- [ ] Moving Camera using mouse.
			- [ ] Zoom towards mouse pointer.
		GameController
			- [ ] Register call to 'Game Paused' Event. Stop gameplay and show menu if _gameIsRunning == false.
			- [ ] Make buildingMenuController singleton.
		Spawn
			- [ ] Spawn enemies from a list.
			- [ ] Set finish point from spawn.
		TowerController
			- [ ] Control the tower object depending on what tower is actually build. Different components?
			- [ ] Implement Range Circle resizing, when shootingRange is changed.
			- [ ] Implement selection.
			- [ ] Implement selection shader.
		TowerModelController
			- [ ] Parse level from model name
			- [ ] Add ChangeModel.
		UnitController
			- [ ] Add unit death animation.
			- [ ] Implement damage to the core event.
	FIXME:

		BuildingMenuController
			- [ ] Remove hardcoded coordinates.
			- [ ] Hide Building Menu when no towers seleted.
		BulletController
			- [ ] Deal with bullets still flying towards destoryed unit.
		GameController
			- [ ] Becoming way too big, refactor.
		HealthBar
			- [ ] Check if null.
		MouseController
			- [ ] Deal with twitchy scroll.
		Shoot
			- [ ] Set by controlling script.
		TowerController
			- [ ] I will assume that 0 lvl tower is base only and does not have Shooting Base or Range Projector.
		TowerModelController
			- [ ] For testing.
			- [ ] ONLY FOR TESTING.
			- [ ] Refactor me.
			- [ ] Fix collider size when object is rotated.
			- [ ] Bounds not doing good. May be depent on the fact it is alway box collider and just code the calculation.
		WalkToTarget
			- [ ] Remove isMoving check from WalkToTarget.
*Code


### Some ideas to try:
- Kingdom Rush mechanics (clone?)

# No updates coming soon. I'm ill
