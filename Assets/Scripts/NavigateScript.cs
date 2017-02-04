using UnityEngine;
using System.Collections;

public class NavigateScript : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

	//scene n° = 0
	public void GoToMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}

	//scene n° = 1
	public void GoToGame()
	{
		Application.LoadLevel("TrainTest");
	}

	//scene n° = 2
	public void GoToCredits()
	{
		Application.LoadLevel("Credits");
	}
}
