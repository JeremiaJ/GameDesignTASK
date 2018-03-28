using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = false;

	// Use this for initialization
	void Start () {
		if (facingRight == true) {
			Flip ();
		}
	}

	public void Flip ()
	{
		// Switch the way the enemy is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.gameObject.tag != "Enemy")
		{
			Destroy (gameObject);
		}

	}
}
