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

	public void PassengerTired (bool tired) {
		if (tired) {
			if (--passengersLeft <= 0) {
				Detach ();
			}
		} else {
			passengersLeft++;
		}
	}

	public void Detach () {
		transform.parent = null;
		StartCoroutine ("GoAway");
	}

	IEnumerator GoAway () {
		for (;;) {
			transform.Translate (driftSpeed*Time.deltaTime, 0f, 0f);
			if (lost)
				break;
			yield return null;
		}
		yield return null;
	}
}
