using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class RetroSnaker : MonoBehaviour
{
	#region private member
	private string _snakeHeadResPath = "SnakeHead";
	private GameObject _snakeHeadPrefab = null;
	private string _snakeNodeResPath = "SnakeNode";
	private GameObject _snakeNodePrefab = null;
	private SnakeNode _snakeHead = null;
	private List<SnakeNode> _snake = new List<SnakeNode>();
	private MoveDir _moveDir;
	private float _timeGap = 1.0f;
	private float _timer = 0.0f;
	private float _timeScaleRef = 0.0f;
	private bool _isInPause = false;
	private List<int> _indexs = new List<int>();
	private int _startNodeCount = 5;
	#endregion private member

	#region mono
	void Awake()
	{
		_snakeHeadPrefab = Resources.Load(_snakeHeadResPath) as GameObject;
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
		if (_isInPause)
			return;
		_timer += Time.deltaTime;
		if (_timer >= _timeGap)
		{
			_timer = 0.0f;
			MoveSnake();
		}
		ChangeMoveDir();
	}

	void OnGUI()
	{
		GUI.Box(new Rect(10, 10, 120, 80), "Score Pad");
		GUI.Label(new Rect(12, 25, 120, 20), string.Format("Scores is {0}.", _snake.Count * 10));
		if (Apple.numberOfObjects == 0)
		{
			GUI.Box(new Rect(200, 100, 120, 80), "Congratulation!");
			GUI.Label(new Rect(220, 120, 120, 20), "You Win!");
			if (!_isInPause)
			{
				_timeScaleRef = Time.timeScale;
				Time.timeScale = 0f;
				_isInPause = true;
			}
			if (GUI.Button(new Rect(220, 140, 60, 20), "Reload"))
			{
				if (_isInPause)
					Time.timeScale = _timeScaleRef;
				SceneManager.LoadScene("RetroSnaker");
			}
		}
	}
	#endregion mono

	#region public interface
	public void OnEatApple()
	{
		GenerateNewNode();
	}
	#endregion public interface

	#region private  implemention
	private void GenerateNewNode()
	{
		GameObject node = GameObject.Instantiate(_snakeNodePrefab);
		SnakeNode nodeComp = node.AddComponent<SnakeNode>();
		SnakeNode last = _snake[_snake.Count - 1];
		Vector3 pos = Vector3.zero;
		switch (last.moveDir)
		{
			case MoveDir.UP:
				pos = last.transform.position + (new Vector3(0f, -SnakeNode.nodeSize, 0));
				break;
			case MoveDir.DOWN:
				pos = last.transform.position + (new Vector3(0f, SnakeNode.nodeSize, 0));
				break;
			case MoveDir.LEFT:
				pos = last.transform.position + (new Vector3(SnakeNode.nodeSize, 0f, 0));
				break;
			case MoveDir.RIGHT:
				pos = last.transform.position + (new Vector3(-SnakeNode.nodeSize, 0f, 0));
				break;
		}
		node.transform.position = pos;
		nodeComp.moveDir = last.moveDir;
		_snake.Add(nodeComp);
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
		GameObject node = GameObject.Instantiate(_snakeHeadPrefab);
		_snakeHead = node.AddComponent<SnakeNode>();
		_snakeHead.moveDir = MoveDir.UP;
		_moveDir = _snakeHead.moveDir;
		_snake.Add(_snakeHead);
		for (int i = 1; i < _startNodeCount; i++)
		{
			GenerateNewNode();
		}
	}
	#endregion private  implemention
}
