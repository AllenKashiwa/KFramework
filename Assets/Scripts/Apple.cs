using UnityEngine;
using System.Collections;

public class Apple : MonoBehaviour
{

	public static int numberOfObjects = 0;
	// Use this for initialization
	void Start()
	{
		++numberOfObjects;
	}

	void OnTriggerEnter(Collider collider)
	{
		Destroy(gameObject);
	}

	void OnDestroy()
	{
		--numberOfObjects;
		RetroSnaker snake = GameObject.FindObjectOfType<RetroSnaker>();
		snake.OnEatApple();
	}

}
