using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
	public int MaxHealth = 3;
	[HideInInspector]
	public int CurrentHealth;
	[HideInInspector]
	public bool HealthChanged; //to give signal to UI

	// Use this for initialization
	void Start () {
		CurrentHealth = MaxHealth;
		HealthChanged = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentHealth <= 0)
			Destroy(this.gameObject); //Handle death here
	}

	public void TakeDamage (int damage) {
		if (damage > 0){
			if (CurrentHealth > 0) {
				CurrentHealth -= damage;
				if (CurrentHealth < 0)
					CurrentHealth = 0;
				HealthChanged = true;
			}
		}
	}

	public void RecoverHealth (int recovery) {
		if (recovery > 0){
			if (CurrentHealth < MaxHealth) {
				CurrentHealth += recovery;
				if (CurrentHealth > MaxHealth)
					CurrentHealth = MaxHealth;
				HealthChanged = true;
			}
		}
	}
}
