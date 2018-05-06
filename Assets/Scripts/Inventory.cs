using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	[Tooltip("The Size should be the same as WeaponTypes available, the same as in InventoryUI Script")]
	public List<int> MaxInventories;
	[Tooltip("The Size should be the same as WeaponTypes available, the same as in InventoryUI Script")]
	public List<int> InitInventories;


	[HideInInspector]
	public List<int> Inventories;
	[HideInInspector]
	public int pointer = 0; //To indicate what weapon the player is using, type 0 is the default weapon, -1 if the inventory is empty
	[HideInInspector]
	public bool PointerChanged; //To give signal to InventoryUI that the player has changed weapon
	[HideInInspector]
	public bool PointerChangedSignal; //To give signal to Gun
	[HideInInspector]
	public bool EmptyInventory; //To indicate that the inventory is empty

	public bool Unlimited;

	// Use this for initialization
	void Start () {
		PointerChanged = false;
		Inventories = new List<int> ();
		foreach (int amount in InitInventories)
			Inventories.Add (amount);
		for (int i = 0; i < Inventories.Count; i++) {
			if (Inventories[i] < 0)
				Inventories[i] = 0;
			if (Inventories[i] > MaxInventories[i])
				Inventories[i] = MaxInventories[i];
		}
		EmptyInventory = IsAllEmpty ();
		if (EmptyInventory) {
			pointer = -1;
			PointerChanged = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void UseAmmo (int usage) {
		if (!EmptyInventory) {
			Inventories [pointer] -= usage;
			if (Inventories [pointer] <= 0) {
				Inventories [pointer] = 0;
				EmptyInventory = IsAllEmpty ();
				if (EmptyInventory) {
					pointer = -1;
					PointerChanged = true;
					PointerChangedSignal = true;
				}
				else
					SwitchRight();
			}
		}
	}

	public void GetWeapon (int type, int amount) {
		Inventories [type] += amount;
		if (Inventories [type] > MaxInventories [type])
			Inventories [type] = MaxInventories[type];
		if (EmptyInventory) {
			EmptyInventory = false;
			pointer = type;
			PointerChanged = true;
			PointerChangedSignal = true;
		}
	}

	public void SwitchLeft () {
		if (!EmptyInventory) {
			do {
				pointer -= 1;
				if (pointer < 0)
					pointer = Inventories.Count - 1;
			} while (Inventories [pointer] <= 0) ;
			PointerChanged = true;
			PointerChangedSignal = true;
		}
	}

	public void SwitchRight () {
		if (!EmptyInventory) {
			do {
				pointer += 1;
				if (pointer >= Inventories.Count)
					pointer = 0;
			} while (Inventories [pointer] <= 0) ;
			PointerChanged = true;
			PointerChangedSignal = true;
		}
	}

	private bool IsAllEmpty () {
		bool empty = true;
		int i = 0;
		while (empty && (i < Inventories.Count)) {
			if (Inventories[i] > 0)
				empty = false;
			else
				i += 1;
		}
		return empty;
	}
}