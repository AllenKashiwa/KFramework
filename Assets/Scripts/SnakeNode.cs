using UnityEngine;
using System.Collections;

public enum MoveDir
{
	UP,
	LEFT,
	RIGHT,
	DOWN
}
public class SnakeNode : MonoBehaviour
{
	public MoveDir moveDir;
	// Use this for initialization
	void Start()
	{
		moveDir = MoveDir.UP;
	}

	private void MoveUp()
	{
		transform.position += new Vector3(0.0f, 1.0f, 0.0f);
	}

	private void MoveDown()
	{
		transform.position += new Vector3(0.0f, -1.0f, 0.0f);
	}

	private void MoveLeft()
	{
		transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
	}

	private void MoveRight()
	{
		transform.position += new Vector3(1.0f, 0.0f, 0.0f);
	}

	public void Move()
	{
		switch (moveDir)
		{
			case MoveDir.UP:
				MoveUp();
				break;
			case MoveDir.DOWN:
				MoveDown();
				break;
			case MoveDir.LEFT:
				MoveLeft();
				break;
			case MoveDir.RIGHT:
				MoveRight();
				break;
		}
	}
}
