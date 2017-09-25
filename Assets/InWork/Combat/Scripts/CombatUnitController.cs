using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum Team {Red, Blue};

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WalkTo))]
public class CombatUnitController : MonoBehaviour {
	public Team team;
	public float AgroRange = 3f;
	public float hitRange = 1f;

	CombatUnitController target;
	bool isWalking;
	bool isAttaking;
	Animator animator;
	WalkTo walkToComponent;

	void Start () {
		CombatController.Instance.AddUnitToTeam (this);
		animator = GetComponent<Animator> ();
		walkToComponent = GetComponent<WalkTo> ();
	}

	void Update(){
		if(isWalking){
			target = CombatController.Instance.FindClosestEnemyInRange (this, AgroRange);
			if (target != null)
				isAttaking = true;
		}
	}
}
