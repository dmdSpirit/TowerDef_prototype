using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	public Transform unitTransform;
	Canvas c;
	void Start(){
		c = transform.parent.GetComponent<Canvas> ();
	}

	void Update(){
		Vector3 oldPos = GetComponent<RectTransform> ().localPosition;
		Vector3 newPos = Camera.main.WorldToViewportPoint (unitTransform.position);
		newPos.z = oldPos.z;
		newPos.x = newPos.x * c.pixelRect.width - c.pixelRect.width * 0.5f;
		newPos.y = newPos.y * c.pixelRect.height - c.pixelRect.height * 0.5f;
		Debug.Log (newPos);
		GetComponent<RectTransform> ().localPosition = newPos;
	}
}
