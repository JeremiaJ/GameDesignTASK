using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
	public GameObject Health1, Health2, Health3;
	public int Health = 3;
	private List<GameObject> Healths;
	private bool TakenDamage;
	private bool HealthRecovered;

	void Awake () {
		Healths = new List<GameObject>();
		Healths.Add (Health1);
		Healths.Add (Health2);
		Healths.Add (Health3);
		foreach (GameObject Health in Healths){
			Health.SetActive (false);
		}
	}

	// Use this for initialization
	void Start () {
		foreach (GameObject Health in Healths){
			Health.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (TakenDamage) {
			TakenDamage = false;
			if (Health > 0) {
				Health -= 1;
				Healths [Health].SetActive (false);
			}
		}
		if (HealthRecovered) {
			HealthRecovered = false;
			if (Health < 3) {
				Healths [Health].SetActive (true);
				Health += 1;
			}
		}
	}

	public void TakeDamage () {
		TakenDamage = true;
	}

	public void RecoverHealth () {
		HealthRecovered = true;
	}
}
