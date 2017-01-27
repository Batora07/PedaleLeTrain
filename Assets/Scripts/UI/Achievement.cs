using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// class of an achievement
public class Achievement : MonoBehaviour {

	private string name;

	private string description;

	private bool unlocked;

	private int points;

	private int spriteIndex;

	private GameObject achievementRef;
	private List<Achievement> dependencies = new List<Achievement>();

	public string Name
	{
		get
		{
			return name;
		}

		set
		{
			name = value;
		}
	}

	public string Description
	{
		get
		{
			return description;
		}

		set
		{
			description = value;
		}
	}

	public bool Unlocked
	{
		get
		{
			return unlocked;
		}

		set
		{
			unlocked = value;
		}
	}

	public int Points
	{
		get
		{
			return points;
		}

		set
		{
			points = value;
		}
	}

	public int SpriteIndex
	{
		get
		{
			return spriteIndex;
		}

		set
		{
			spriteIndex = value;
		}
	}

	public string Child
	{
		get
		{
			return child;
		}

		set
		{
			child = value;
		}
	}

	private string child;

	public Achievement(string name, string description, int points, int spriteIndex, GameObject achievementRef)
	{
		this.Name = name;
		this.Description = description;
		this.Points = points;
		this.unlocked = false;
		this.SpriteIndex = spriteIndex;
		this.achievementRef = achievementRef;
		LoadAchievements();
	}

	public void AddDependency(Achievement dependency)
	{
		dependencies.Add(dependency);
	}

	
	public bool EarnAchievement()
	{
		if (!Unlocked && !dependencies.Exists(x => x.unlocked == false))
		{
			achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
			SaveAchievement(true);

			if(child != null)
			{
				AchievementManager.Instance.EarnAchievement(child);
			}

			return true;
		}

		return false;
	}

	public void SaveAchievement(bool value)
	{
		Unlocked = value;

		int tmpPoints = PlayerPrefs.GetInt("Points");

		PlayerPrefs.SetInt("Points", tmpPoints += points);

		PlayerPrefs.SetInt(name, value ? 1 : 0);

		PlayerPrefs.Save();
	}

	public void LoadAchievements()
	{
		unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;

		if (unlocked)
		{
			AchievementManager.Instance.textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
			achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
		}
	}

}
