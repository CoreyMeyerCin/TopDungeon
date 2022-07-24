using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doughnut : MonoBehaviour, ICollectible
{
	private readonly int attackSpeedIncreasePercent = 5;

	public static event Action OnDoughtnutCollected;
	public void OnCollect()
	{
		Player.instance.stats.attackSpeed = Player.instance.stats.IncreaseByPercentageOfCurrent(Player.instance.stats.attackSpeed, attackSpeedIncreasePercent);
		Destroy(gameObject);
		OnDoughtnutCollected?.Invoke();
	}
}
