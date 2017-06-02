using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Unit health managment, death event.
/// </summary>

public class Health : MonoBehaviour {
	public float health = 3f;

	public event Action<GameObject> OnDeath;

	public void TakeDamage(float damage){
		health = Mathf.Max (0, health - damage);

		if (health == 0 && OnDeath != null)
			OnDeath(gameObject);
	}
}
