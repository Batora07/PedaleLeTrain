using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour {
	public Wagon[] wagons = new Wagon[3];
	private int currentWagon = 0;
	public Passenger[] passengers = new Passenger[6];

	void Start () {
	}

	public void WagonDetached () {
		if (++currentWagon >= wagons.Length) {
		}
	}

	public void Trick (bool success) {

	}
	
	void Update () {
	}
}
