using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDagger : MonoBehaviour, ICollectible
{
	public static event Action OnThrowingDaggerCollected;
	public void OnCollect()
	{
		//update equipped weapon
		//update player sprite
		Destroy(gameObject);
		OnThrowingDaggerCollected?.Invoke();
	}
}
