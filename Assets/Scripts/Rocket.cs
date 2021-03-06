﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Rocket : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
	public static int score = 0;
	public static GameObject c;

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 4);
	}


	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		if((col.gameObject.tag != "Player") && (col.gameObject.tag != "Item") && (col.gameObject.tag != "Bullet"))
		{
			score = score + 2;
			// Instantiate the explosion and destroy the rocket.
			OnExplode();
			Destroy (gameObject);
		}

	}
}
