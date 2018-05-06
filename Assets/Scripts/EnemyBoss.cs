using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour {

	public Rigidbody2D flame;				// Prefab of the flame.
	public float fireRange = 25f;			// Enemy fire range
	private float fireRate = 2f;
	private float speed = 20f;				// The speed the rocket will fire at.

	public bool facingRight = false;
	private Animator anim;                  // Reference to the Animator component.
	private Transform player;				// Reference to the player's transform.

	private float chargeRange = 20f;			// Enemy charge range
	private float maxSpeed = 2.5f;				// Enemy charge speed
	private float moveForce = 200f;			// Enemy move force

	private float meleeRate = 3f;			// Enemy charge rate
	private float meleeRange = 3f;			// Enemy charge range
	private BoxCollider2D col;

	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator> ();
		col = GetComponent<BoxCollider2D> ();
	}

	float CheckRange ()
	{
		return player.position.x - transform.position.x;
	}

	bool IsInFireRange ()
	{
		float range = CheckRange ();
		return Mathf.Abs(range) < fireRange && Mathf.Abs(range) > meleeRange;
	}

	bool IsInChargeRange ()
	{
		float range = CheckRange ();
		return Mathf.Abs(range) < chargeRange;
	}

	bool IsInMeleeRange ()
	{
		float range = CheckRange ();
		return Mathf.Abs(range) < meleeRange;
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
		// charge direction
		float h = 0;
		if (IsInChargeRange ()) {
			anim.SetBool ("charge", true);
			if (IsPlayerRight ()) {
				h = 1;
			} else {
				h = -1;
			}
		} else {
			anim.SetBool ("charge", false);
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
		if (IsInFireRange ()) {
			bool fireDirection = IsPlayerRight ();

			Vector3 shootDirection;
			shootDirection = (player.position - transform.position).normalized;

			Vector3 currentPosition = transform.position;
			if (fireDirection) {
				currentPosition.x += 1;
			} else {
				currentPosition.x -= 1;
			}

			moveForce = 0f;
			anim.SetTrigger ("throw");
			yield return new WaitForSeconds (0.2f);
			Rigidbody2D bulletInstance = Instantiate(flame, currentPosition, transform.rotation);
			bulletInstance.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
			// Destroy the rocket after 4 seconds if it doesn't get destroyed before then.
			Destroy(bulletInstance.gameObject, 4);

			// fire
			yield return new WaitForSeconds (0.2f);
			Rigidbody2D bulletInstance2 = Instantiate(flame, currentPosition, transform.rotation);
			bulletInstance2.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
			// Destroy the rocket after 4 seconds if it doesn't get destroyed before then.
			Destroy(bulletInstance2.gameObject, 4);

			yield return new WaitForSeconds (0.5f);
			moveForce = 200f;
		}
		yield return new WaitForSeconds (fireRate);
		StartCoroutine (Shoot ());
	}

	IEnumerator Melee() {
		// charge direction
		Vector2 colSize = col.size;
		if (IsInMeleeRange ()) {
			col.size = new Vector2 (8f, colSize.y);
			anim.SetTrigger ("melee");
			yield return new WaitForSeconds (1f);
			col.size = new Vector2 (colSize.x, colSize.y);
		}
		yield return new WaitForSeconds (meleeRate);
		StartCoroutine (Melee ());
	}

	void Start ()
	{
		StartCoroutine (Shoot ());
		StartCoroutine (Melee ());
	}
}
