using UnityEngine;
using System.Collections;

public class Wagon : MonoBehaviour {
	private Train train;
	private bool lost = false;
	public float driftSpeed = -4f;
	private int passengersLeft = 2;

	void Awake () {
		train = transform.parent.gameObject.GetComponent<Train> ();
	}

	public void LostPassenger () {
		if (--passengersLeft <= 0) {
			Detach ();
		}
	}

	public void Detach () {
		transform.parent = null;
		StartCoroutine ("GoAway");
	}

	IEnumerator GoAway () {
		for (;;) {
			transform.Translate (driftSpeed, 0f, 0f);
			if (lost)
				break;
			yield return null;
		}
		yield return null;
	}
}
