using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOWController : MonoSingleton<FOWController> {
	[SerializeField]
	GameObject FOWPlane;
	[SerializeField]
	GameObject FOWCamera;
	[SerializeField]
	GameObject HouseVisionCircle;

	void Start () {
		CheckIsSingleInScene ();
		FOWPlane.SetActive (true);
		HouseVisionCircle.SetActive (true);

		foreach (var t in GameObject.FindGameObjectsWithTag("FoW"))
			t.SetActive (true);
	}
}
