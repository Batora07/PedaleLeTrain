using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour {

	public GameObject achievementPrefab;

	public Sprite[] sprites; // array of sprites images for the achievements

	public AchievementButton activeButton;

	public ScrollRect scrollRect;

	public GameObject achievementMenu;

	public GameObject visualAchievement;

	public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

	public Sprite unlockedSprite;

	private static AchievementManager instance;

	public Text textPoints;

	private int fadeTime = 2;

	public static AchievementManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<AchievementManager>();
			}
			return AchievementManager.instance;
		}
	}

	void Start () {
		// --- DEBUG OPTIONS ---
		PlayerPrefs.DeleteAll();
		//PlayerPrefs.DeleteKey("Points");
		
		activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievementButton>();

		CreateAchievement("General","SPACE ! SPAAACE !", "Congratulations, you made your first jump !", 5, 0);
		CreateAchievement("General", "Press S", "S to rejoice.", 5, 0);
		CreateAchievement("General", "All keys", "All keys to rejoice.", 10, 0, new string[] { "SPACE ! SPAAACE !", "Press S" } );


		foreach (GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList"))
		{
			achievementList.SetActive(false);
		}

		activeButton.Click();

		achievementMenu.SetActive(false);
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.I))
		{
			achievementMenu.SetActive(!achievementMenu.activeSelf);
		}
		// achievement 1 = spaaaace
		if (Input.GetButton("Jump"))
		{
			EarnAchievement("SPACE ! SPAAACE !");
		}
		// achievement 2 = S
		if (Input.GetKeyDown(KeyCode.S))
		{
			EarnAchievement("Press S");
		}
	}

	public void EarnAchievement(string title)
	{
		if (achievements[title].EarnAchievement())
		{
			// we just earned a new achievement, awesome ;) !
			GameObject achievement = (GameObject)Instantiate(visualAchievement);
			SetAchievementInfo("EarnCanvas", achievement, title);

			textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");

			StartCoroutine(FadeAchievement(achievement));
		}
	}

	public IEnumerator HideAchievement(GameObject achievement)
	{
		// just wait for 4 sec then remove the  temporary achievement on the screen 
		yield return new WaitForSeconds(4);
		Destroy(achievement);
	}

	public void CreateAchievement(string parent, string title, string description, int points, int spriteIndex, string[] dependencies = null)
	{
		GameObject achievement = (GameObject)Instantiate(achievementPrefab);
		Achievement newAchievement = new Achievement(name, description, points, spriteIndex, achievement);

		achievements.Add(title, newAchievement);

		SetAchievementInfo(parent, achievement, title);

		if(dependencies != null)
		{
			foreach(string achievementTitle in dependencies)
			{
				Achievement dependency = achievements[achievementTitle];
				dependency.Child = title;
				newAchievement.AddDependency(dependency);
			}
		}

	}

	public void SetAchievementInfo(string parent, GameObject achievement, string title)
	{
		// give it the category to find and in which parent it should be
		achievement.transform.SetParent(GameObject.Find(parent).transform);
		achievement.transform.localScale = new Vector3(1, 1, 1);
		// get title (child 0 of achiev prefab)
		achievement.transform.GetChild(0).GetComponent<Text>().text = title;
		// get description (child 1 of achiev prefab)
		achievement.transform.GetChild(1).GetComponent<Text>().text = achievements[title].Description;
		// get points (child 2 of achiev prefab)
		achievement.transform.GetChild(2).GetComponent<Text>().text = achievements[title].Points.ToString();
		// get image (child 3 of achiev prefab)
		achievement.transform.GetChild(3).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
	}

	public void ChangeCategory(GameObject button)
	{
		AchievementButton achievementButton = button.GetComponent<AchievementButton>();
		scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform>();

		achievementButton.Click();
		activeButton.Click();

		activeButton = achievementButton;
	}

	private IEnumerator FadeAchievement(GameObject achievement)
	{
		// we get the canvas group associated to the previewed achievement
		CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup>();

		float rate = 1.0f / fadeTime;

		int startAlpha = 0;
		int endAlpha = 1;

		for (int i = 0; i < 2; i++)
		{
			float progress = 0.0f;
			while (progress < 1.0)
			{
				canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

				progress += rate * Time.deltaTime;

				yield return null;
			}
			yield return new WaitForSeconds(2);
			startAlpha = 1;
			endAlpha = 0;
		}
		Destroy(achievement);
	}
}
