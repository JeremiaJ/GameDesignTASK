using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.
	public static Animator animator;
	private float atGunTime = 0.34f;
	private float atGun;
	private bool loadingGun = false;
    private float moveForce = 365f;          // Amount of force added to move the player left and right.
    private float maxSpeed = 7.5f;             // The fastest the player can travel in the x axis.
    private float jumpForce = 1050f;         // Amount of force added when the player jumps.
    public float tauntProbability = 50f;    // Chance of a taunt happening.
    public float tauntDelay = 1f;           // Delay for when the taunt should happen.
	public bool invicible = false;
	// private int counter = 3;
    private int tauntIndex;                 // The index of the taunts array indicating the most recent taunt.
    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    private bool grounded = false;          // Whether or not the player is grounded.
	public static int perk = 3;					// Default perk 1 = hp up, 2 = walk up, 3 = project up, 4 = pickup up
	public bool getObjective = false;
	private Health CharHealth;
	private Inventory CharInv;

    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
		animator = GetComponent<Animator> ();
		CharHealth = GetComponent<Health>();
		CharInv = GetComponent<Inventory>();
		invicible = false;
		// Hp up perk
		Debug.Log (perk);
		if (perk == 1) {
			// counter = 4;
			CharHealth.MaxHealth = 4;
		} else 
		if (perk == 2) {
			moveForce = 550f;
		} else 
		if (perk == 3) {
			Gun.speedmodifier = 1.5f;
		}
    }


    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));




        // If the jump button is pressed and the player is grounded then the player should jump.
		if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && grounded){
			animator.SetBool ("isJumping", true);
			jump = true;
		}
            
    }
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Item") {
			int type = col.gameObject.GetComponent<itemDestroy>().type;
			if (type == 3)
				CharHealth.RecoverHealth(2);
			else
				CharInv.GetWeapon(type, 3);
		}
		if (col.gameObject.tag == "Enemy") {
			if (!invicible) {
				invicible = true;
				CharHealth.TakeDamage(1);
				animator.SetBool ("isHit", true);
				StartCoroutine (DamageTaken ());
			}
		}
	}

	IEnumerator DamageTaken() {
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.1f);
		animator.SetBool ("isHit", false);
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.1f);
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.1f);
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.1f);
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.1f);
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.1f);
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.1f);
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.1f);
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.1f);
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.1f);
		invicible = false;
	}

	// IEnumerator OnTriggerEnter2D (Collider2D col) 
	// {
	// 	if(col.gameObject.tag == "Enemy")
	// 	{
	// 		if (!invicible) {
	// 			invicible = true;
	// 			counter = counter - 1;
	// 			if (counter == 0) {
	// 				Destroy (gameObject);
	// 			}
	// 			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	// 			yield return new WaitForSeconds (0.1f);
	// 			this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	// 			yield return new WaitForSeconds (0.1f);
	// 			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	// 			yield return new WaitForSeconds (0.1f);
	// 			this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	// 			yield return new WaitForSeconds (0.1f);
	// 			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	// 			yield return new WaitForSeconds (0.1f);
	// 			this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	// 			yield return new WaitForSeconds (0.1f);
	// 			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	// 			yield return new WaitForSeconds (0.1f);
	// 			this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	// 			yield return new WaitForSeconds (0.1f);
	// 			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	// 			yield return new WaitForSeconds (0.1f);
	// 			this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	// 			yield return new WaitForSeconds (0.1f);
	// 			invicible = false;
	// 		}

			
	// 		// Instantiate the explosion and destroy the rocket.

	// 	}

	// }

    void FixedUpdate()
    {
        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");
		if (h == 0) {
			animator.SetBool ("isWalking", false);
		} else {
			animator.SetBool("isWalking", true);
		}
        // The Speed animator parameter is set to the absolute value of the horizontal input.

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            // ... add a force to the player.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();

        // If the player should jump...
        if (jump)
        {
			
            // Set the Jump animator trigger parameter.


            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
			animator.SetBool ("isJumping", false);
        }
    }


    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
		transform.localScale = theScale;
    }

}
