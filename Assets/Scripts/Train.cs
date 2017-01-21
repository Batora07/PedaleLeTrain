using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour {
	public Wagon[] wagons = new Wagon[3];
	public Passenger[] passengers = new Passenger[6];
	private int currentWagon = 0;
	private int currentPassenger = 0;
	public static int level {
		get;
		private set;
	}

	public void WagonDetached () {
		if (++currentWagon >= wagons.Length) {
			Debug.Log ("Lost");
		}
	}

	public void Trick (bool success) {
		Debug.Log ("Trick : " + success);
		Debug.Log ("Passenger : " + currentPassenger);
		Debug.Log ("Wagon : " + currentWagon);
		if (success) {
			if (currentPassenger / 2 >= currentWagon && currentPassenger > 0) {
				passengers[currentPassenger--].SetTired (false);
				level--;
			}
		} else {
			passengers[currentPassenger++].SetTired (true);
			if ((currentWagon + 1) * 2 == currentPassenger) {
				wagons[currentWagon++].Detach ();
			}
			level++;
		}
	}


}
