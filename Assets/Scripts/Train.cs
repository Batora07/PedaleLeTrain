using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Train : MonoBehaviour {
	public Wagon[] wagons = new Wagon[3];
	public Passenger[] passengers = new Passenger[6];
	private int currentWagon = 0;
	public static int level {
		get;
		private set;
	}
	public static int score {
		get;
		private set;
	}
	public static bool lost {
		get;
		private set;
	}
	public float[] scales = new float[6] { .5f, .6f, .7f, .8f, .9f, 1f };

	void Start () {
		level = 0;
		score = 0;
		lost = false;

		StartCoroutine (LateStart (.1f));
	}

	IEnumerator LateStart (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		ChangeSpeed ();
	}

	public void DetachWagon () {
		wagons[currentWagon].Detach ();
		currentWagon++;
		if (currentWagon >= wagons.Length) {
			Debug.Log ("Lost : " + score);
			lost = true;
			Time.timeScale = 0f;
		}
	}

	public void Trick (bool success, int value) {//*
		Debug.Log ("------------------");
		Debug.Log ("Level : " + level);
		Debug.Log ("Wagon : " + currentWagon);//*/
		if (success) {
			if (level > 0 && level / 2 == (level - 1) / 2) {
				level--;
				passengers[level].SetTired (false);
			}
			score += value;
		} else {
			passengers[level].SetTired (true);
			if (level / 2 != (level + 1) / 2) {
				DetachWagon ();
			}
			level++;
		}
	}

	public void ChangeSpeed () {
		DecorScrolling[] scrolling = GameObject.FindObjectsOfType<DecorScrolling> ();
		for (int i = 0; i < scrolling.Length; i++) {
			scrolling[i].scale = scales[level];
		}
	}
}
