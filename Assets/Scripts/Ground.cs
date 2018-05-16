using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	private bool isOnGround = true;

	void OnTriggerEnter2D() {
		isOnGround = true;
	}

	void OnTriggerStay2D() {
		isOnGround = true;
	}

	void OnTriggerExit2D(){
		isOnGround = false;
	}

	public bool OnGround(){
		return isOnGround;
	}
}
