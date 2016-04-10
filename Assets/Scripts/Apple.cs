using UnityEngine;
using System.Collections;

public class Apple : MonoBehaviour
{
	public static int numberOfObjects = 0;

	#region mono
	// Use this for initialization
	void Start()
	{
		++numberOfObjects;
	}

	void OnTriggerEnter(Collider collider)
	{
		gameObject.SetActive(false);
		--numberOfObjects;
		RetroSnaker snake = GameObject.FindObjectOfType<RetroSnaker>();
		if (snake != null && snake.isActiveAndEnabled)
			snake.OnEatApple();
	}
	#endregion mono

}
