using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		ICollectible collectible = collision.GetComponent<ICollectible>();
		if (collectible != null)
		{
			GameManager.instance.ShowText("F", 25, Color.yellow, collision.transform.position, Vector3.zero, 0.5f);
			if (Input.GetKey(KeyCode.F))
			{
				collectible.OnCollect();
			}
		}
	}
}
