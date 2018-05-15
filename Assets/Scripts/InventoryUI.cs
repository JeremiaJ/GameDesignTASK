using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
	// public Image Weapon;
	public Inventory CharInventory;
	[Tooltip("The Size should be the same as WeaponTypes available, the same as in Inventory Script")]
	public List<Sprite> WeaponsUI;
	public Sprite EmptyUI;
	public Text AmmoIndicator;
	private int WeaponTypes;

	// Use this for initialization
	void Start () {
		WeaponTypes = WeaponsUI.Count;
	}
	
	// Update is called once per frame
	void Update () {
		if (CharInventory.PointerChanged) {
			CharInventory.PointerChanged = false;
			if (CharInventory.pointer != -1){
				this.GetComponent<Image>().sprite = WeaponsUI [CharInventory.pointer];
				AmmoIndicator.text = CharInventory.Inventories[CharInventory.pointer].ToString();
			}
			else
				this.GetComponent<Image>().sprite = EmptyUI;
		}
		if (CharInventory.AmmoChanged){
			if (!CharInventory.EmptyInventory)
				AmmoIndicator.text = CharInventory.Inventories[CharInventory.pointer].ToString();
			else
				AmmoIndicator.text = 0.ToString();
			CharInventory.AmmoChanged = false;
		}
	}
}
