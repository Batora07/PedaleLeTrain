using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	[SerializeField]
	private Animator settingsButtonAnim;

	private bool hidden;
	private bool canTouchSettingsButton;

	[SerializeField]
	private Button musicBtn;

	[SerializeField]
	private Sprite[] musicBtnSprites;

	[SerializeField]
	private Button fbBtn;

	[SerializeField]
	private Sprite[] fbSprites;

	[SerializeField]
	private GameObject infoPanel;

	void Start()
	{
		canTouchSettingsButton = true;
		hidden = true;

		if (GameController.instance.isMusicOn)
		{
			MusicScript.instance.PlayBGMusic();
			musicBtn.image.sprite = musicBtnSprites[0];
		}
		else
		{
			MusicScript.instance.StopBGMusic();
			musicBtn.image.sprite = musicBtnSprites[1];
		}
	}

	public void SettingsButton()
	{
		StartCoroutine(DisableSettingsButtonWhilePlayingAnimation());
	}

	IEnumerator DisableSettingsButtonWhilePlayingAnimation()
	{
		if (canTouchSettingsButton)
		{
			if (hidden)
			{
				canTouchSettingsButton = true;
				settingsButtonAnim.Play("SlideIn");
				hidden = false;
				yield return new WaitForSeconds(1.2f);
				canTouchSettingsButton = true;
			}
			else
			{
				canTouchSettingsButton = true;
				settingsButtonAnim.Play("SlideOut");
				hidden = true;
				yield return new WaitForSeconds(1.2f);
				canTouchSettingsButton = true;
			}
		}
	}

	public void MusicButton()
	{
		if (GameController.instance.isMusicOn)
		{
			musicBtn.image.sprite = musicBtnSprites[1];
			MusicScript.instance.StopBGMusic();
			GameController.instance.isMusicOn = false;
			GameController.instance.Save();
		}
		else
		{
			musicBtn.image.sprite = musicBtnSprites[0];
			MusicScript.instance.PlayBGMusic();
			GameController.instance.isMusicOn = true;
			GameController.instance.Save();
		}
	}

	public void OpenInfoPanel()
	{
		infoPanel.SetActive(true);
	}

	public void CloseInfoPanel()
	{
		infoPanel.SetActive(false);
	}

}
