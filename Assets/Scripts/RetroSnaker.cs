using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RetroSnaker : MonoBehaviour
{
	private string _snakeNodeResPath = "SnakeNode";
	private GameObject _snakeNodePrefab = null;
	private List<SnakeNode> _snake = new List<SnakeNode>();
	private MoveDir _moveDir;
	private float _timeGap = 1.0f;
	private float _timer = 0.0f;
	private List<int> _indexs = new List<int>();
	void Awake()
	{
		_snakeNodePrefab = Resources.Load(_snakeNodeResPath) as GameObject;
	}

	// Use this for initialization
	void Start()
	{
		RetroSnakerStart();
	}

	// Update is called once per frame
	void Update()
	{
		_timer += Time.deltaTime;
		if (_timer >= _timeGap)
		{
			_timer = 0.0f;
			MoveSnake();
		}
		ChangeMoveDir();
	}

	private void MoveSnake()
	{
		for (int i = 0; i < _snake.Count; i++)
		{
			SnakeNode node = _snake[i];
			node.Move();
		}
		List<int> tempIndexs = new List<int>();
		for (int i = 0; i < _indexs.Count; i++)
		{
			int index = _indexs[i];
			if (index > 0 && index < _snake.Count)
			{
				_snake[index].moveDir = _snake[index - 1].moveDir;
				_indexs[i]++;
			}
			if (_indexs[i] < _snake.Count)
				tempIndexs.Add(_indexs[i]);
		}
		_indexs = tempIndexs;
	}

	private void ChangeMoveDir()
	{
		if (Input.GetButtonDown("UP"))
		{
			if (_moveDir == MoveDir.DOWN)
				return;
			_moveDir = MoveDir.UP;
		}
		else if (Input.GetButtonDown("LEFT"))
		{
			if (_moveDir == MoveDir.RIGHT)
				return;
			_moveDir = MoveDir.LEFT;
		}
		else if (Input.GetButtonDown("RIGHT"))
		{
			if (_moveDir == MoveDir.LEFT)
				return;
			_moveDir = MoveDir.RIGHT;
		}
		else if (Input.GetButtonDown("DOWN"))
		{
			if (_moveDir == MoveDir.UP)
				return;
			_moveDir = MoveDir.DOWN;
		}
		if (_moveDir != _snake[0].moveDir)
		{
			_snake[0].moveDir = _moveDir;
			_timer = _timeGap + 1f;
			_indexs.Add(1);
		}
		//Debug.Log("index count = " + _indexs.Count);
	}

	private void RetroSnakerStart()
	{
		GameObject node = GameObject.Instantiate(_snakeNodePrefab);
		SnakeNode head = node.GetComponent<SnakeNode>();
		_moveDir = head.moveDir;
		_snake.Add(head);
		GenerateNewNode();
		GenerateNewNode();
		GenerateNewNode();
		GenerateNewNode();
	}

	private void GenerateNewNode()
	{
		GameObject node = GameObject.Instantiate(_snakeNodePrefab);
		SnakeNode last = _snake[_snake.Count - 1];
		Vector3 pos = Vector3.zero;
		switch (last.moveDir)
		{
			case MoveDir.UP:
				pos = last.transform.position + (new Vector3(0f,-1f,0));
				break;
			case MoveDir.DOWN:
				pos = last.transform.position + (new Vector3(0f, 1f, 0));
				break;
			case MoveDir.LEFT:
				pos = last.transform.position + (new Vector3(1f, 0f, 0));
				break;
			case MoveDir.RIGHT:
				pos = last.transform.position + (new Vector3(-1f, 0f, 0));
				break;
		}
		node.transform.position = pos;
		_snake.Add(node.GetComponent<SnakeNode>());
	}
}
