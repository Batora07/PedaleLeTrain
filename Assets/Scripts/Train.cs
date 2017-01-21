using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour {
	public Wagon[] wagons = new Wagon[3];
	public Passenger[] passengers = new Passenger[6];
	private int currentWagon = 0;
	public static int level {
		get;
		private set;
	}

	void Start () {
	}

	public void WagonDetached () {
		if (++currentWagon >= wagons.Length) {
		}
	}

	public void Trick (bool success) {
		if (!success) {
			passengers[level].SetTired (true);
			level++;
		}
	}
	
	void Update () {
	}
}
