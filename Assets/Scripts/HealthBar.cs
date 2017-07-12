using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	public Transform unitTransform;

	[Range(0,1)]
	public float healthPercent = 1f;

	RectTransform healthImageRectTransform; 

	Canvas c;
	void Start(){
		c = transform.parent.GetComponent<Canvas> ();

		// FIXME: Check if null.
		healthImageRectTransform = transform.Find ("HealthImage").GetComponent<RectTransform>();
		HealthChanged (healthPercent);
	}

	void Update(){
		Vector3 oldPos = GetComponent<RectTransform> ().localPosition;
		Vector3 newPos = Camera.main.WorldToViewportPoint (unitTransform.position);
		newPos.z = oldPos.z;
		newPos.x = newPos.x * c.pixelRect.width - c.pixelRect.width * 0.5f;
		newPos.y = newPos.y * c.pixelRect.height - c.pixelRect.height * 0.5f;
		//Debug.Log (newPos);
		GetComponent<RectTransform> ().localPosition = newPos;



	}

	// Change health bar.
	public void HealthChanged(float newHealthPercent){
		healthImageRectTransform.offsetMax = new Vector2((healthPercent-1)*100,0);
	}
}
