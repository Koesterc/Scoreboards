using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class UserHandler : MonoBehaviour {

	public List<Text> usersTexts = new List<Text> ();
	public List<User>users = new List<User> ();
	
	public IEnumerator GetUsers()
	{
		WWW response = new WWW ("http://localhost:2222/api/users");
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
					users.Add (JsonUtility.FromJson<User> (tempString));
					if (i <11)//only adding the first 10 to the leaderboards
						usersTexts [i].text = users[i].Name;
					print (users[i].Name);
					i++;
				}
			}
		}
	}

	public IEnumerator PostPlayer()
	{
		User playerToPost = new User (GameManager.gameManager.userName);
		UnityWebRequest www = UnityWebRequest.Post("http://localhost:2222/api/users", "");
		UploadHandler customUploadHandler = new UploadHandlerRaw (System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(playerToPost)));
		customUploadHandler.contentType = "application/json";
		www.uploadHandler = customUploadHandler;
		yield return www.Send ();

		string playerIDString = www.GetResponseHeader ("Location").Substring(32);
	//	PlayerID = System.Convert.ToInt32 (playerIDString);

		if (www.error != null)
			Debug.Log ("Error: " + www.error);
		else
			Debug.Log ("Success: " + www.responseCode);	
	}

}
