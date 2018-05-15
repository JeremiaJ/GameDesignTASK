using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRandomizer : MonoBehaviour {
	public itemDestroy Item;
	public List<float> WeaponChances;
	public List<Sprite> WeaponSprites;

	void Awake(){
		float sum = 0;
		foreach(float chance in WeaponChances)
			sum += chance;
		float gacha = Random.Range (0f, sum);

		float low = 0f;
		float high = WeaponChances[0];
		int i = 0;
		bool found = false;
		while (!found) {
			found = ((gacha >= low) && (gacha < high));
			if (!found){
				i +=1 ;
				low = high;
				high += WeaponChances[i];
			}
		}
		Item.type = i;
		GetComponent<SpriteRenderer>().sprite = WeaponSprites[i];
	}
}
