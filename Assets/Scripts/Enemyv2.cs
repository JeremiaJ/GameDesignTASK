using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyv2 : MonoBehaviour {

	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int HP = 2;					// How many times the enemy can be hit before it dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.

	private Score score;				// Reference to the Score script.
	public Rigidbody2D item1;
	public float chanceDrop1 = 0f;
	public Rigidbody2D item2;
	public float chanceDrop2 = 0f;
	public Rigidbody2D item3;
	public float chanceDrop3 = 0f;


	void Awake()
	{
		// Setting up the references.
		score = GameObject.Find("Score").GetComponent<Score>();
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.gameObject.tag == "Bullet")
		{
			// Instantiate the explosion and destroy the rocket.
			Hurt ();
		}

	}

	public void Hurt()
	{
		// Reduce the number of hit points by one.
		HP--;
		if (HP <= 0) {
			Death ();
		}
	}

	void Death()
	{
		DropItem ();
		Destroy (gameObject);
	}

	void DropItem()
	{
		Vector3 currentPosition = transform.position;
		currentPosition.y += 5;
		if (Random.Range (0f, 1f) <= chanceDrop1) {
			Instantiate(item1, currentPosition, transform.rotation);
		}
		if (Random.Range (0f, 1f) <= chanceDrop2) {
			Instantiate (item2, currentPosition, transform.rotation);
		}
		if (Random.Range (0f, 1f) <= chanceDrop3) {
			Instantiate (item3, currentPosition, transform.rotation);
		}
	}
}
