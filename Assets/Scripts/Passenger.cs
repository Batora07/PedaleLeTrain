using UnityEngine;
using System.Collections;

public class Passenger : MonoBehaviour {
	private Wagon wagon;
	private bool tired;

	void Awake () {
		wagon = transform.parent.gameObject.GetComponent<Wagon> ();
	}

	public void SetTired (bool value) {
		if (tired != (tired = value)) {
			GetComponent<Animator> ().Play (tired ? "tired" : "pedalling");
		}
	}
	
	void Update () {
	
	}
}
