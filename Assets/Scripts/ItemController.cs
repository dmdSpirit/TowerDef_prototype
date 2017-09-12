using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ItemController : MonoBehaviour {
	Rigidbody rigidbidy;
	BoxCollider collider;
	GameObject character;

	bool isDead = false;

	void Start(){
		collider = GetComponent<BoxCollider> ();
		rigidbidy = GetComponent<Rigidbody> ();
		collider.enabled = false;
		rigidbidy.useGravity = false;
		rigidbidy.isKinematic = true;
	}

	public void OnCharacterDeath(GameObject character){
		this.character = character;
		if(isDead == false)
			StartCoroutine ("DestroyItem");
	}

	IEnumerator DestroyItem(){
		isDead = true;
		yield return new WaitForSeconds (0.2f);
		transform.SetParent (character.transform.parent, true);
		collider.enabled = true;
		rigidbidy.useGravity = true;
		rigidbidy.isKinematic = false;
		yield return new WaitForSeconds (4);
		Destroy (gameObject);
	}
}
