using UnityEngine;
using System.Collections;

public class Passenger : MonoBehaviour {
	private bool tired;

	public void SetTired (bool value) {
		if (tired != (tired = value)) {
			GetComponent<Animator> ().Play (tired ? "tired" : "pedalling");
		}
	}

	public void ChangeSpeed () {

	}
}
