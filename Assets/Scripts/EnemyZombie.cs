using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : MonoBehaviour {


	public Rigidbody2D flame;				// Prefab of the flame.
	public float fireRate = 3f;
	private float speed = 25f;				// The speed the rocket will fire at.
	public bool facingRight = false;

	private Animator anim;                  // Reference to the Animator component.

	private Transform player;				// Reference to the player's transform.
	public float attackRange = 15f;			// Enemy attack range
	private int health = 3;
	public float moveRange = 30f;			// Enemy move range
	public float maxSpeed = 2f;				// Enemy move speed
	private float moveForce = 365f;			// Enemy move force

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.gameObject.tag == "Bullet")
		{
			health = health - 1;
			// Instantiate the explosion and destroy the rocket.
			if (health < 1) {
				Destroy (gameObject);
			}

		}

	}

	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator>();
	}

	float CheckRange ()
	{
		return player.position.x - transform.position.x;
	}

	bool IsInAttackRange ()
	{
		float range = CheckRange ();
		return Mathf.Abs(range) < attackRange;
	}

	bool IsInMoveRange ()
	{
		float range = CheckRange ();
		return Mathf.Abs(range) < moveRange;
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

		float h = 0;
		if (IsInMoveRange ()) {
			anim.SetBool ("walk", true);
			if (IsPlayerRight ()) {
				h = 1;
			} else {
				h = -1;
			}
		} else {
			anim.SetBool ("walk", false);
		}

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}

	IEnumerator Shoot() {
		// fire direction
		if (IsInAttackRange ()) {
			bool fireDirection = IsPlayerRight ();

			Vector3 shootDirection;
			shootDirection = (player.position - transform.position).normalized;
			shootDirection.y += 0.45f;

			Vector3 currentPosition = transform.position;
			if (fireDirection) {
				currentPosition.x += 1;
			} else {
				currentPosition.x -= 1;
			}

			moveForce = 0f;
			anim.SetTrigger ("throw");
			yield return new WaitForSeconds (0.4f);

			// fire
			Rigidbody2D bulletInstance = Instantiate(flame, currentPosition, transform.rotation);
			bulletInstance.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
			// Destroy the rocket after 4 seconds if it doesn't get destroyed before then.
			Destroy(bulletInstance.gameObject, 4);

			moveForce = 365f;
		}
		yield return new WaitForSeconds (fireRate);
		StartCoroutine (Shoot ());
	}

	void Start ()
	{
		StartCoroutine (Shoot ());
	}
}
