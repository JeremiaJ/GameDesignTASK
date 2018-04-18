using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
	public GameObject HealthInit;
	public float HealthUIDistance;
	public int MaxHealth = 3;
	private int Health;
	private List<GameObject> HealthsUI;

	// Use this for initialization
	void Start () {
		Health = MaxHealth;

		HealthsUI = new List<GameObject> ();
		HealthsUI.Add(HealthInit);
		GameObject Temp;
		Vector3 position = new Vector3 (50, 0, 0);
		for (int i = 1; i < MaxHealth; i++) {
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

	}

	public void TakeDamage () {
		if (Health > 0) {
			Health -= 1;
			HealthsUI[Health].SetActive(false);
//			LocalCanvas.HealthsUI [Health].SetActive (false);
		}
	}

	public void RecoverHealth () {
		if (Health < MaxHealth) {
			HealthsUI[Health].SetActive(true);
//			LocalCanvas.HealthsUI [Health].SetActive (true);
			Health += 1;
		}
	}
}
