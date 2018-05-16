using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	private bool isOnGround = false;

	void OnTrigerStay2D() {
		isOnGround = true;
	}

	void OnTrigerExit2D(){
		isOnGround = false;
	}

	public bool OnGround(){
		return isOnGround;
	}
}
