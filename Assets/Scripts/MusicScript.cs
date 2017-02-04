using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
	public static MusicScript instance;
	private AudioSource bgMusic;
	private float time;

	void Awake()
	{
		MakeSingleton();
		AudioSource[] audioSources = GetComponents<AudioSource>();
		//When the scene loads it checks if there is an object called "MUSIC".
		bgMusic = audioSources[0];
	}

	void MakeSingleton() {  
		if(instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void StopBGMusic()
	{
		if (bgMusic.isPlaying)
		{
			bgMusic.Stop();
		}
	}

	private void OnLevelWasLoaded(int level)
	{
		if(Application.loadedLevelName == "MainMenu")
		{
			if (GameController.instance.isMusicOn)
			{
				if (!bgMusic.isPlaying)
				{
					bgMusic.time = time;
					bgMusic.Play();
				}
			}
		}
	}

	public void GameIsLoadedTurnOfMusic()
	{
		if (bgMusic.isPlaying)
		{
			time = bgMusic.time;
			bgMusic.Stop();
		}
	}

	public void PlayBGMusic()
	{
		if (!bgMusic.isPlaying)
		{
			bgMusic.Play();
		}
	}
}


