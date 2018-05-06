using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour {
	public GameObject HealthUI;
	public Inventory Test;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.LeftBracket))
			HealthUI.GetComponent<HealthUI>().TakeDamage ();
		if (Input.GetKeyUp (KeyCode.RightBracket))
			HealthUI.GetComponent<HealthUI>().RecoverHealth ();
		if (Input.GetKeyUp (KeyCode.Semicolon))
			Test.SwitchLeft();
		if (Input.GetKeyUp (KeyCode.Quote))
			Test.SwitchRight();
		if (Input.GetKeyUp (KeyCode.Period))
			Test.UseAmmo(1);
		if (Input.GetKeyUp (KeyCode.Slash))
			Test.GetWeapon(1, 1);
	}
}
