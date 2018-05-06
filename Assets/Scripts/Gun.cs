﻿using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket array.
	public float speed = 1f;				// The speed the rocket will fire at.
	public static float speedmodifier = 1f;
	public bool facingRight = true;
	private string[] weaponList = new string[]{"projectile1", "projectile2", "projectile3"};
	// private int currentWeapon = 0;
	private MovePlayer playerCtrl;		// Reference to the PlayerControl script.
	private Inventory CharInv;
	private Animator anim;                  // Reference to the Animator component.
    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * 25;
    }

    void Awake()
	{
		playerCtrl = transform.root.GetComponent<MovePlayer>();
		CharInv = transform.root.GetComponent<Inventory>();
		speed = 2f;				// The speed the rocket will fire at.
	}


	void Update ()
	{
		facingRight = playerCtrl.facingRight;
		// If the fire button is pressed (right click)...
		if(Input.GetButtonDown("Fire2")){
			CharInv.SwitchRight();
		}
		//If the weapon is switched, whether manually or automatically when it that type of weapon runs out
		if (CharInv.PointerChangedSignal){
			GameObject asset = Resources.Load (weaponList[CharInv.pointer]) as GameObject;
			rocket = asset.GetComponent<Rigidbody2D>();
			CharInv.PointerChangedSignal = false;
		}
		// If the fire button is pressed (left click)...
		if(Input.GetButtonDown("Fire1"))
		{
			if (!CharInv.EmptyInventory){
				// To check whether need to flip the player or not based on mouse click
				Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
				Vector3 PlayerPos = Camera.main.WorldToScreenPoint (playerCtrl.transform.position);
				// Di kiri player
				if (mouse.x < PlayerPos.x) {
					if (facingRight) {
						playerCtrl.Flip ();
						facingRight = !facingRight;
					}
				} else {
					// di Kanan Player
					if (!facingRight) {
						playerCtrl.Flip ();
						facingRight = !facingRight;
					}
				}
	            Vector3 shootDirection;
	            shootDirection = Input.mousePosition;
	            shootDirection.z = 0.0f;
	            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
	            shootDirection = shootDirection - transform.position;
	            //...instantiating the rocket
	            Vector3 vel = GetForceFrom(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

	            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
				//float angle2 = Mathf.Atan2(vel.y + 25, vel.x) * Mathf.Rad2Deg;
				//float angle3 = Mathf.Atan2(vel.y - 25, vel.x) * Mathf.Rad2Deg;
				// if (currentWeapon == 0) {
	            if (CharInv.pointer == 0) {
					// Keripik Kentang
					Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed * speedmodifier);
				// } else if (currentWeapon == 1) {
				} else if (CharInv.pointer == 1) {
					// Pilus
					Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as Rigidbody2D;
					Rigidbody2D bulletInstance2 = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as Rigidbody2D;
					Rigidbody2D bulletInstance3 = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(shootDirection.x * speed * 1.2f, shootDirection.y * speed * 1.0f * speedmodifier);
					bulletInstance2.velocity = new Vector2(shootDirection.x * speed * 1.2f, shootDirection.y * speed * 1.5f * speedmodifier);
					bulletInstance3.velocity = new Vector2(shootDirection.x * speed * 1.2f, shootDirection.y * speed * 0.5f * speedmodifier);
				// } else if (currentWeapon == 2) {
				} else if (CharInv.pointer == 2) {
					// Bomb
					Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(shootDirection.x * speed * 0.8f, shootDirection.y * speed * 0.8f * speedmodifier);
				}
				//Rigidbody2D bulletInstance2 = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, angle2))) as Rigidbody2D;
				//Rigidbody2D bulletInstance3 = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, angle3))) as Rigidbody2D;
				if (!CharInv.Unlimited)
					CharInv.UseAmmo(1);
			}
		}
	}
}
