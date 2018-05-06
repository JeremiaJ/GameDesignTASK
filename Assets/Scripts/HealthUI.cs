using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
	public GameObject HealthInit;
	public float HealthUIDistance;
	public Health CharHealth;
	private int TempHealth;
	private List<GameObject> HealthsUI;

	// Use this for initialization
	void Start () {
		TempHealth = CharHealth.MaxHealth;

		HealthsUI = new List<GameObject> ();
		HealthsUI.Add(HealthInit);
		GameObject Temp;
		Vector3 position = new Vector3 (HealthUIDistance, 0, 0);
		for (int i = 1; i < CharHealth.MaxHealth; i++) {
			Temp = new GameObject ("Health");
			Temp.AddComponent<RectTransform> ();
			Temp.AddComponent<CanvasRenderer> ();
			Temp.AddComponent<Image> ();
			Temp.transform.SetParent(HealthInit.transform);
			Temp.layer = 5;
			Temp.transform.localPosition = position;
			position.x += HealthUIDistance;
			Temp.GetComponent<RectTransform>().sizeDelta = HealthInit.GetComponent<RectTransform>().sizeDelta;
			Temp.GetComponent<Image> ().sprite = HealthInit.GetComponent<Image>().sprite;
			HealthsUI.Add (Temp);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(CharHealth.HealthChanged){
			CharHealth.HealthChanged = false;
			int deltaHealth = TempHealth - CharHealth.CurrentHealth;
			Debug.Log(deltaHealth);
			if (deltaHealth < 0) {
				for (int i = 0; i < (-deltaHealth); i++)
					RecoverHealthUI();
			} else if (deltaHealth > 0){
				for (int i = 0; i < deltaHealth; i++)
					TakeDamageUI();
			}
		}
	}

	public void TakeDamageUI () {
		TempHealth -= 1;
		HealthsUI[TempHealth].SetActive(false);
	}

	public void RecoverHealthUI () {
		HealthsUI[TempHealth].SetActive(true);
		TempHealth += 1;
	}
}
