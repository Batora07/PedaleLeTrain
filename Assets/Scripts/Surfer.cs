using UnityEngine;
using System.Collections;

public class Surfer : MonoBehaviour {
	public float fallSpeed = 0f;
	public float maxFallSpeed = -6f;
	public float turnRateLine = 90f;
	private Quaternion targetRotation;
	private int grounded = 0;
	private bool turning = false;
	private bool jumping = false;

	public int currentSpeed = 0;
	public float[] speeds = new float[6] { 4f, 2f, 3f, 4f, 5f, 6f };
	public float[] maxFallSpeeds = new float[6];
	public float[] gravities = new float[6] { 9.81f, 9.81f, 9.81f, 9.81f, 9.81f, 9.81f };
	public float[] turnRatesJump = new float[6] { 360.0f, 360.0f, 360.0f, 360.0f, 360.0f, 360.0f };

	void Start () {
	}
	
	void Update () {
		if (Input.GetButtonDown ("Jump") && grounded > 0) {
			fallSpeed = speeds[currentSpeed];
			grounded = 0;
			jumping = true;
		}
		if (jumping) {
			if (Input.GetButton ("Jump")) {
				transform.RotateAround (GetComponent<Collider2D> ().bounds.center, Vector3.back, -turnRatesJump[currentSpeed] * Time.deltaTime);
			} else {
				float angle = Quaternion.Angle (transform.rotation, Quaternion.identity);/*
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.identity, turnRatesJump[currentSpeed] / angle * Time.deltaTime);/*/
				if (angle > 2.0f)
					transform.RotateAround (GetComponent<Collider2D> ().bounds.center, Vector3.back, (transform.right.y > 0 ? 1f : -1f) * turnRatesJump[currentSpeed] * Time.deltaTime);//*/
			}
		}
		if (grounded > 0) {
			fallSpeed = Mathf.Max (0f, fallSpeed);
		} else {
			fallSpeed -= gravities[currentSpeed] * Time.deltaTime;
			fallSpeed = Mathf.Max (maxFallSpeed, fallSpeed);
		}
		transform.position += new Vector3 (speeds[currentSpeed] * Time.deltaTime, grounded > 0 ? 0f : fallSpeed * Time.deltaTime);
		if (grounded > 0 || turning) {
			float angle = Quaternion.Angle (transform.rotation, targetRotation);
			if (angle <= 2.0f) {
				turning = false;
			} else {
				//Debug.Log (Quaternion.Angle (transform.rotation, targetRotation) + " : " + turnRate / angle * Time.deltaTime);
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, turnRateLine / angle * Time.deltaTime);
			}
		}
	}
	//*
	void OnCollisionEnter2D (Collision2D coll) {
		//Debug.Log (coll.gameObject);
		if (jumping) {
			Debug.Log (Vector3.Angle (transform.right, coll.transform.up));
		}
		if (fallSpeed < 0) {
			turning = true;
			jumping = false;
		}
		if (grounded > 0) {
			targetRotation = Quaternion.Lerp (coll.gameObject.transform.rotation * Quaternion.Euler (0f, 0f, 90f), targetRotation, 0.5f);
		} else {
			targetRotation = coll.gameObject.transform.rotation * Quaternion.Euler (0f, 0f, 90f);
		}
		grounded++;
	}

	void OnCollisionExit2D (Collision2D coll) {
		if (grounded > 0)
			grounded--;
		/*
		if (coll.contacts[0].point.y > transform.position.y) {
			transform.position = new Vector3 (transform.position.x, coll.contacts[0].point.y);
		}//*/
	}
	/*/
	void OnTriggerEnter2D (Collider2D other) {
		turning = true;
		if (grounded > 0) {
			targetRotation = Quaternion.Lerp (other.transform.rotation * Quaternion.Euler (0f, 0f, 90), targetRotation, 0.5f);
		} else {
			targetRotation = other.transform.rotation * Quaternion.Euler (0f, 0f, 90);
		}
		grounded++;
	}

	void OnTriggerExit2D (Collider2D other) {
		if (grounded > 0)
			grounded--;
	}//*/
}
