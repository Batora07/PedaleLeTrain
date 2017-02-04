using UnityEngine;
using System.Collections;

public class PlayerMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void GoToMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}
}
