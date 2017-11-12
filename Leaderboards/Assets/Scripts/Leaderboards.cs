using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Leaderboards : MonoBehaviour {

	public List<Text> namesTexts = new List<Text> ();
	public ScoreHandler scoreHandler;
	public UserHandler userHandler;
	public GameObject scoreBoards;

	// Use this for initialization
	public void GameOver() {
		StartCoroutine (Wait());
	}

	IEnumerator Wait()
	{
		StartCoroutine (userHandler.PostPlayer());
		yield return new WaitForSeconds (3f);
		scoreHandler.Order ();
		userHandler.SortUsers();
		scoreBoards.SetActive (true);
		Time.timeScale = 0;
	}
}
		