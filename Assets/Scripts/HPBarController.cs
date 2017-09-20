using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour {
	[SerializeField]
	Color emptyHP = new Color ();
	[SerializeField]
	Color fullHP = new Color();
	[SerializeField]
	GameObject hpGO;

	public int maxHP;
	int _currentHP;
	public int currentHP{
		get {return _currentHP;}
		set {
			if(value != _currentHP){
				_currentHP = value;
				for (int i = 0; i < maxHP; i++)
					transform.GetChild (i).GetComponent<Image> ().color = i < value ? fullHP : emptyHP;
			}
		}
	}
	
	public void InitHP(int maxHP){
		this.maxHP = maxHP;
		for(int i=0; i<maxHP-1; i++){
			GameObject newHP = Instantiate (hpGO, transform);
			newHP.name = "HP";
		}
		currentHP = maxHP;
	}
}
