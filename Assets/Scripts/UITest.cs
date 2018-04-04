using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour {
	public HealthUI CanvasUI;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.LeftBracket))
			CanvasUI.TakeDamage ();
		if (Input.GetKeyUp (KeyCode.RightBracket))
			CanvasUI.RecoverHealth ();
	}
}
