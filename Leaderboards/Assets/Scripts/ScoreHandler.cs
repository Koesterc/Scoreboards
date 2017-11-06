﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreHandler : MonoBehaviour {

	public List<Text> scoresTexts = new List<Text> ();
	public List<Score>scores = new List<Score> ();
	
	public IEnumerator GetScores()
	{
		WWW response = new WWW ("http://localhost:2222/api/scores");
		yield return response;
		if (response.error != null)
			Debug.Log (response.error);
		else {
			//remove square brackets
			string responseString = response.text.Substring(1, response.text.Length - 2);
			//split on commas
			string[] substrings = responseString.Split('{');
			//pass each string into from json
			int i = 0;
			foreach (string sub in substrings) {
				//store all of that data in a list of scores
				if(sub != "")
				{
					string tempString = "{" + sub;

					if(tempString[tempString.Length - 1] == ',')
						tempString = tempString.Substring(0, tempString.Length - 1);
					scores.Add (JsonUtility.FromJson<Score> (tempString));
					if (i <11)//only adding the first 10 to the leaderboards
						scoresTexts [i].text = scores [i].Score1.ToString ("n0");
					print (scores[i].Score1);
					i++;
				}
			}
		}
	}
}
