using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	public void Pop()
	{
		Respawn ();
		GameManager.gameManager.scoreManager.score += 100;
		GameManager.gameManager.scoreManager.scoreText.text = "Score "+GameManager.gameManager.scoreManager.score.ToString("n0");

	}

	void Respawn()
	{
		int y = Random.Range (530,700);
		int x = Random.Range (-900,900);
		transform.localPosition = new Vector2 (x,y);
	}

	void Update()
	{
		if (transform.localPosition.y < -520) {
			print (GameManager.gameManager);
			GameManager.gameManager.scoreManager.lives--;
			GameManager.gameManager.scoreManager.livesText.text = "Lives "+GameManager.gameManager.scoreManager.lives.ToString("n0");

			Respawn ();
		}
	}

}
