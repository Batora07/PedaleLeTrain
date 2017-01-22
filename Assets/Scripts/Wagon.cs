using UnityEngine;
using System.Collections;

public class Wagon : MonoBehaviour {
	public float driftSpeed = -4f;

	public void Detach () {
		transform.parent = null;
		StartCoroutine ("GoAway");
	}

	IEnumerator GoAway () {
		for (;;) {
			transform.Translate (driftSpeed*Time.deltaTime, 0f, 0f);
			if (Train.lost) {
				Destroy (gameObject);
				break;
			}
			yield return null;
		}
		yield return null;
	}
}
