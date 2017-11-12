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
		StartCoroutine (scoreHandler.PostScore());
		StartCoroutine (userHandler.PostPlayer());
		StartCoroutine (Wait());
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds (1f);
		scoreBoards.SetActive (true);
		Time.timeScale = 0;
	}
}
		