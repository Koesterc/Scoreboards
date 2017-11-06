using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	[SerializeField]
	private GameManager m_gameManager;
	public static GameManager gameManager;
	public ScoreManager scoreManager;
	public ScoreManager leaderboards;
	public Score score;
	public User player;
	public string userName;

	void Awake()
	{
		gameManager = m_gameManager;
		Debug.Log (m_gameManager);
	}
}






[System.Serializable]
public class Score
{
	public int ScoreID;
	public int UserID;
	public int Score1;

	public Score(int _userID, int _score1)
	{
		UserID = _userID;
		Score1 = _score1;
	}
}
	
[System.Serializable]
public class User
{
	public int UserID;
	public string Name;

	public User(string _userName)
	{
		Name = _userName;
	}
}