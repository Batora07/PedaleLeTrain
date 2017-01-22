using UnityEngine;
using System.Collections;

public class Passenger : MonoBehaviour {
	private bool tired;

	void Awake () {
		ChangeSpeed ();
	}

	public void SetTired (bool value) {
		if (tired != (tired = value)) {
			GetComponent<Animator> ().Play (tired ? "tired" : "pedalling");
			if (!tired)
				ChangeSpeed ();
		}
	}

	public void ChangeSpeed () {
		GetComponent<Animator> ().speed = Random.Range (.25f, 2f);
	}
}
