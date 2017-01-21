using UnityEngine;
using System.Collections;

public class Passenger : MonoBehaviour {
	private Wagon wagon;

	void Awake () {
		wagon = transform.parent.gameObject.GetComponent<Wagon> ();
	}
	
	void Update () {
	
	}
}
