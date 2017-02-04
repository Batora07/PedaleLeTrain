using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public bool paused;
    private GameObject textPauseGame;
    private GameObject pauseRestartInput;
	private GameObject button_return;
	private GameObject button_replay;

	private UIManager() { }

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        paused = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        textPauseGame = GameObject.Find("PauseTextMessageOnScreen");
        pauseRestartInput = GameObject.Find("PauseRestartInput");
		button_replay = GameObject.Find("Button_replay");
		button_return = GameObject.Find("Button_return");

		if (textPauseGame != null){
			Time.timeScale = 1;
			textPauseGame.GetComponent<Text>().enabled = false;
			button_replay.SetActive(false);
			button_return.SetActive(false);
		}

    }

    //Reloads the Level
    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Pause()
    {
        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0;
            textPauseGame.GetComponent<Text>().enabled = true;
			button_replay.SetActive(true);
			button_return.SetActive(true);
			pauseRestartInput.GetComponent<Image>().sprite = Resources.Load<Sprite>("Play_button");
        }
        else if (!paused)
        {
            Time.timeScale = 1;
			button_replay.SetActive(false);
			button_return.SetActive(false);
			textPauseGame.GetComponent<Text>().enabled = false;
            pauseRestartInput.GetComponent<Image>().sprite = Resources.Load<Sprite>("Pause_button");
        }

    }

    public void Play()
    {
		Invoke("PlayTheGame", 3.0f);
    }

	// needs delay to create Beziers curves
	public void PlayTheGame()
	{
		SceneManager.LoadScene(1);
	}

    public void ShowCredits()
    {
        SceneManager.LoadScene(2);
    }
}
