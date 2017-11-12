using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;

public class ScoreHandler : MonoBehaviour {

	public List<Text> scoresTexts = new List<Text> ();
	public List<Score>scores = new List<Score> ();
	//used to sort
	public List<Score> sortedScores = new List<Score>();
	public UserHandler userHandler;
	
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
					scores.Add (JsonUtility.FromJson<Score> (tempString));//**
					//scores.OrderByDescending(x => x.Score1);
					i++;
				}
			}
		}
	}

	public void Order()
	{//order lst form highest to lowest
		List<Score> trackerList = new List<Score>();

		foreach (Score s in scores) {
			trackerList.Add (s);
		}

		for (int i = 0; i < scores.Count; i++) 
		{
			Score element = trackerList [0];
			int count = 0;

			for (int j = 0; j < trackerList.Count; j++) 
			{
				if (trackerList [j].Score1 > element.Score1) 
				{
					element = trackerList [j];
					count = j;
				}
			}
			trackerList.RemoveAt(count);
			sortedScores.Add (element);
		}
		for (int i = 0; i < sortedScores.Count; i++)
		{
			if (i <11)//only adding the first 10 to the leaderboards
				scoresTexts [i].text = sortedScores[i].Score1.ToString ("n0");
		}
	}
		
	public IEnumerator PostScore()
	{
		Score scoreToPost = new Score(userHandler.userID,GameManager.gameManager.scoreManager.score);
		UnityWebRequest www = UnityWebRequest.Post("http://localhost:2222/api/scores", "");
		UploadHandler customUploadHandler = new UploadHandlerRaw (System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(scoreToPost)));
		customUploadHandler.contentType = "application/json";
		www.uploadHandler = customUploadHandler;
		yield return www.Send ();

		if (www.error != null)
			Debug.Log ("Error: " + www.error);
		else
			Debug.Log ("Success: " + www.responseCode);	
		StartCoroutine(GetScores());
	}
}
