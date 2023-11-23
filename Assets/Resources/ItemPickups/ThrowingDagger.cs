using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDagger : MonoBehaviour, ICollectable
{
	public Weapon weapon;
	public void OnCollect()
	{
		if (weapon != null)
		{
			Actions.OnWeaponChanged(weapon);
		}
		else
		{
			Debug.LogError("weapon component not found on collected gameobject");
		}
		Destroy(gameObject); //this may cause issues if it is being hit before the weapon is fully equipped and the weapon var is only a reference
	}

}
