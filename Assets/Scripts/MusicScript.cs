using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
	void Awake()
	{
		DontDestroyOnLoad(GameObject.Find("Soundscape"));
	}
}
