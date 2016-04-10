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
	#region public members
	public MoveDir moveDir;
	public static float nodeSize = 1.0f;
	#endregion public members

	#region public interface
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
	#endregion public interface

	#region private implemention
	private void MoveUp()
	{
		transform.position += new Vector3(0.0f, nodeSize, 0.0f);
	}

	private void MoveDown()
	{
		transform.position += new Vector3(0.0f, -nodeSize, 0.0f);
	}

	private void MoveLeft()
	{
		transform.position += new Vector3(-nodeSize, 0.0f, 0.0f);
	}

	private void MoveRight()
	{
		transform.position += new Vector3(nodeSize, 0.0f, 0.0f);
	}
	#endregion private implemention
}
