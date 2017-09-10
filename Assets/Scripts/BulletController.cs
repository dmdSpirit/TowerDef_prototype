using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
	float speed;
	UnitController target;
	float damage;

	void Update () {
		// FIXME: Deal with bullets still flying towards destoryed unit.
		if (target != null && target.isAlive)
			transform.position = Vector3.MoveTowards (transform.position, target.shootingTarget.position, 
				Time.deltaTime * speed);
		else
			Destroy (gameObject);
	}

	public void Init(UnitController target, float speed, float damage){
		this.target = target;
		this.speed = speed;
		this.damage = damage;
	}

	void OnTriggerEnter(Collider unitCollider){
		//Debug.Log (gameObject.name + "::OnCollisionEnter");
		if(unitCollider.gameObject.tag == "Enemy"){
			Health enemyHealth = unitCollider.gameObject.GetComponent<Health> ();
			if (enemyHealth != null)
				DealDamage (enemyHealth);
		}
	}

	void DealDamage(Health enemyHealth){
		//Debug.Log(enemyHealth.gameObject.name+" was hit.");
		enemyHealth.TakeDamage (damage);
		Destroy (gameObject);
	}
}
