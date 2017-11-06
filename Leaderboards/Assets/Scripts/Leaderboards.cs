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

	// Use this for initialization
	void Start () {
		//StartCoroutine (scoreHandler.GetScores());
		//StartCoroutine (userHandler.GetUsers());
		StartCoroutine (userHandler.PostPlayer());
	}
}
		