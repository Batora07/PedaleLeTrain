using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
	void Awake()
	{
		Debug.Log("AWAAAAKE");
		DontDestroyOnLoad(GameObject.Find("Soundscape"));
	}
}
