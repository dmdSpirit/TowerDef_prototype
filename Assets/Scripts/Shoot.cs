using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shooting bullets towards the target, handling shooting cd.
/// </summary>

public class Shoot : MonoBehaviour {
	public GameObject bulletPrefab; // FIXME: Set by controlling script.
	[SerializeField]
	float shootingCD = 0.2f;
	public float bulletDamage = 1f;
	public float bulletSpeed = 6f;

	float timePassed;
	TowerController towerController;

	void Start(){
		timePassed = 0;
		towerController = GetComponent<TowerController> ();
	}

	void Update(){
		timePassed += Time.deltaTime;
		if(timePassed >= shootingCD && towerController.target != null && towerController.shootingBase!=null){
			UnitController unitController = towerController.target.GetComponent<UnitController> ();
			if (unitController != null) {
				GameObject newBullet = Instantiate (bulletPrefab, towerController.shootingBase.position, Quaternion.identity);
				newBullet.GetComponent<BulletController> ().Init (unitController, bulletSpeed, bulletDamage);
				timePassed = 0;
			}
		}
	}
}
