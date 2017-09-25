using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoSingleton<CombatController> {
	public List<CombatUnitController> redTeamList;
	public List<CombatUnitController> blueTeamList;

	void Start () {
		CheckIsSingleInScene ();
	}
	
	public void AddUnitToTeam(CombatUnitController unit){
		if (unit.team == Team.Red)
			redTeamList.Add (unit);
		else
			blueTeamList.Add (unit);
	}

	public CombatUnitController FindClosestEnemyInRange(CombatUnitController unit, float range){
		List<CombatUnitController> enemyList;
		enemyList = unit.team == Team.Red ? blueTeamList : redTeamList;
		float closestRange = Mathf.Infinity;
		float distance;
		CombatUnitController target = null;
		foreach(var enemy in enemyList){
			distance = Vector3.Distance (enemy.transform.position, unit.transform.position);
			if(distance < range && distance < closestRange){
				target = enemy;
				closestRange = distance;
			}
		}
		return closestRange < range ? target : null;
	}
}
