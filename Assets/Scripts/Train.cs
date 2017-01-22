using UnityEngine;
using System.Collections.Generic;

public class Train : MonoBehaviour {
	public Wagon[] wagons = new Wagon[3];
	public Passenger[] passengers = new Passenger[6];
	private int currentWagon = 0;
	public static int level {
		get;
		private set;
	}
	public static bool lost {
		get;
		private set;
	}
	public float[] scales = new float[6] { .5f, .6f, .7f, .8f, .9f, 1f };
	private DecorScrolling[] scrolling;

	void Start () {
		level = 0;
		lost = false;
		scrolling = GameObject.FindObjectsOfType<DecorScrolling> ();
		Debug.Log (scrolling.Length);
		ChangeSpeed ();
	}

	public void DetachWagon () {
		wagons[currentWagon].Detach ();
		currentWagon++;
		if (currentWagon >= wagons.Length) {
			Debug.Log ("Lost");
			lost = true;
		}
	}

	public void Trick (bool success) {//*
		Debug.Log ("------------------");
		Debug.Log ("Level : " + level);
		Debug.Log ("Wagon : " + currentWagon);//*/
		if (success) {
			if (level > 0 && level / 2 == (level - 1) / 2) {
				level--;
				passengers[level].SetTired (false);
			}
		} else {
			passengers[level].SetTired (true);
			if (level / 2 != (level + 1) / 2) {
				DetachWagon ();
			}
			level++;
		}
	}

	public void ChangeSpeed () {
		for (int i = 0; i < scrolling.Length; i++) {
			scrolling[i].scale = scales[level];
		}
	}
}
