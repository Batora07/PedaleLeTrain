using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

	public GameObject achievementList;

	public Sprite neutral, highlight;

	private Image sprite;

	private void Awake()
	{
		sprite = GetComponent<Image>();
	}

	void Start () {
	
	}
	
	public void Click()
	{
		if(sprite.sprite == neutral)
		{
			sprite.sprite = highlight;
			//shows the category 
			achievementList.SetActive(true);
		}
		else
		{
			sprite.sprite = neutral;
			achievementList.SetActive(false);
		}
	}
}
