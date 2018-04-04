using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {


	public Rigidbody2D flame;				// Prefab of the flame.
	public float fireRate = 1f;
	public float speed = 10f;				// The speed the rocket will fire at.
	public bool facingRight = false;

	private Animator anim;                  // Reference to the Animator component.

	private Transform player;				// Reference to the player's transform.
	public float attackRange = 25f;			// Enemy attack range

	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	float CheckRange ()
	{
		return player.position.x - transform.position.x;
	}

	bool IsInRange ()
	{
		float range = CheckRange ();
		return Mathf.Abs(range) < attackRange;
	}

	bool IsPlayerRight ()
	{
		float range = CheckRange ();
		return range > 0;
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

	void FixedUpdate ()
	{
		if (facingRight != IsPlayerRight ()) {
			Flip ();
		}
	}

	IEnumerator Shoot() {
		// fire direction
		if (IsInRange ()) {
			bool fireDirection = IsPlayerRight ();

			Vector3 shootDirection;
			shootDirection = (player.position - transform.position).normalized;

			// fire
			Rigidbody2D bulletInstance = Instantiate(flame, transform.position, transform.rotation);
			bulletInstance.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
			// Destroy the rocket after 4 seconds if it doesn't get destroyed before then.
			bulletInstance.GetComponent<EnemyBullet>().facingRight = IsPlayerRight();
			Destroy(bulletInstance.gameObject, 4);
		}
		yield return new WaitForSeconds (fireRate);
		StartCoroutine (Shoot ());
	}

	void Start ()
	{
		StartCoroutine (Shoot ());
	}
}
