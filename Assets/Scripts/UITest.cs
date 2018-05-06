using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour {
	public Health TestHealth;
	public Inventory TestInv;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.LeftBracket))
			TestHealth.TakeDamage (2);
		if (Input.GetKeyUp (KeyCode.RightBracket))
			TestHealth.RecoverHealth (2);
		if (Input.GetKeyUp (KeyCode.Semicolon))
			TestInv.SwitchLeft();
		if (Input.GetKeyUp (KeyCode.Quote))
			TestInv.SwitchRight();
		if (Input.GetKeyUp (KeyCode.Period))
			TestInv.UseAmmo(1);
		if (Input.GetKeyUp (KeyCode.Slash))
			TestInv.GetWeapon(1, 1);
	}
}
