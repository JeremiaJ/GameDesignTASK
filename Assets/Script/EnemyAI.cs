using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]

public class EnemyAI : MonoBehaviour {

	// What to chase
	public Transform target;
	// How many times each second we will update path
	public float updateRate = 2f;

	// Caching
	private Seeker seeker;
	private Rigidbody2D rb;

	// The calculated path 
	public Path path;

	// The AI's speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	private bool pathIsEnded = false;

	// The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;
	// Waypoint we are currently moving towards (represented by index)
	private int currentWaypoint = 0;

	void Start () {
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
			Debug.LogError ("No player found");
			return;
		}
		// Start a new path to target position and delegate path to OnPathComplete
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());
	}

	public void OnPathComplete (Path p) {
		Debug.Log ("Path catced, have error?" + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	IEnumerator UpdatePath () {
		if (target == null) {
			Debug.LogError ("No player found");
			yield break;
		}
		// Start a new path to target position and delegate path to OnPathComplete
		seeker.StartPath (transform.position, target.position, OnPathComplete);
		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());
	}

	void FixedUpdate () {
		if (target == null) {
			// TODO: insert player search here
			return;
		}

		if (path == null) {
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}

			Debug.Log ("End of path reached");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		// TODO: always look at player

		// Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir = dir * speed * Time.fixedDeltaTime;

		// Move the AI
		rb.AddForce (dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);
		if (dist < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
}
