using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBulletBounce : MonoBehaviour 
{
	public static int score = 0;
	public static GameObject c;
	// Bounce 5 times
	public int bounceCounter = 3;

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
	}

	void Update () {
		transform.Rotate (0, 0, 180 * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.gameObject.tag == "Player") {
			Destroy (gameObject);
		} else if (col.gameObject.tag == "Bullet") {
			Destroy (gameObject);
		} else if ((col.gameObject.tag != "Enemy") && (col.gameObject.tag != "Item") && (col.gameObject.tag != "Bullet")) {
			bounceCounter--;
			if (bounceCounter == 0) {
				Destroy (gameObject);
			}
		}

	}
}
