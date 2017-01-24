using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scorePoint : MonoBehaviour {

	public Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText.enabled = true;
		scoreText.text = "Tricks Points :" + Train.score;
	}
	
}
