using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if(collider.gameObject.tag == "Player")
		{
			// Instantiate the explosion and destroy the rocket.
			Destroy (gameObject);
		}
	}
}
