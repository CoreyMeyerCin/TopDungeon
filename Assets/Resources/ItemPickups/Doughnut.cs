using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doughnut : MonoBehaviour, ICollectible
{
	private readonly int attackSpeedIncreasePercent = 5;

	public static event Action OnDoughnutCollected;
	public void OnCollect()
	{
		Player.instance.stats.attackSpeed = Player.instance.stats.IncreaseByPercentageOfCurrent(Player.instance.stats.attackSpeed, attackSpeedIncreasePercent);
		Debug.Log($"Attack speed increased to {Player.instance.stats.attackSpeed}");
		Destroy(gameObject);
		//OnDoughnutCollected?.Invoke();
	}
}
