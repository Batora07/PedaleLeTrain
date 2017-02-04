using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class scorePoint : MonoBehaviour {

	public Text scoreText;
	private int highScore;
	//DateTime now;

	// Use this for initialization
	void Start () {
		scoreText.enabled = true;
		//now = DateTime.Now;
		//string hours = now.ToString("HH:mm");
		scoreText.text = "Tricks Points :" + Train.score;

		//Set/Get the highscore with Player Prefs
		/*		if (!PlayerPrefs.HasKey("Score"))
				{
					PlayerPrefs.SetInt("Score", 0);
				}
				else {
					highScore = PlayerPrefs.GetInt("Score");

					if(highScore < Train.score)
					{
						//PlayerPrefs.SetInt("Score", Train.score);
					}
				}
				//	Debug.Log("Highscore : " + PlayerPrefs.GetInt("Score"));

		*/
		highScore = GameController.instance.highScore;
		GameController.instance.currentScore = Train.score;
		Debug.Log("Current score = " + GameController.instance.currentScore);
		Debug.Log("High score = " + GameController.instance.highScore);

		if (Train.score > highScore)
		{
			GameController.instance.highScore = GameController.instance.currentScore;
			GameController.instance.Save();
		}


		// We get the current irl hour
		//Debug.Log(hours);
	}

}
