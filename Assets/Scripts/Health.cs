using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Unit health managment, death event.
/// </summary>

public class Health : MonoBehaviour {
	[SerializeField]
	HPBarController hpBar;

	public int health = 3;

	public event Action<GameObject> OnDeath;

	void Start(){
		hpBar.InitHP (health);
	}

	public void TakeDamage(int damage){
		health = Mathf.Max (0, health - damage);
		hpBar.currentHP = health;
		if (health == 0 && OnDeath != null) {
			OnDeath (gameObject);
			hpBar.gameObject.SetActive (false);
		}
	}
}
