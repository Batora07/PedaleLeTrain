using UnityEngine;
using System.Collections;

public class Surfer : MonoBehaviour {
	public float speed = 4f;
	public float jumpSpeed = 5f;
	public float fallSpeed = 0f;
	public float maxFallSpeed = -6f;
	public float gravity = -9.81f;
	public float turnRate = 90.0f;
	private Quaternion targetRotation;
	private int grounded = 0;
	private bool turning = false;
	private bool jumping = false;

	public int currentSpeed = 0;
	public float[] speeds = new float[6];
	public float[] maxFallSpeeds = new float[6];
	public float[] gravities = new float[6];
	public float[] turnRates = new float[6];

	void Start () {
	}
	
	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			fallSpeed = jumpSpeed;
			grounded = 0;
		}
		if (grounded > 0) {
			fallSpeed = Mathf.Max (0f, fallSpeed);
		} else {
			fallSpeed += gravity * Time.deltaTime;
			fallSpeed = Mathf.Max (maxFallSpeed, fallSpeed);
		}
		transform.position += new Vector3 (speed * Time.deltaTime, grounded > 0 ? 0f : fallSpeed * Time.deltaTime);
		if (grounded > 0 || turning) {
			float angle = Quaternion.Angle (transform.rotation, targetRotation);
			if (angle <= 2.0f) {
				turning = false;
			} else {
				//Debug.Log (Quaternion.Angle (transform.rotation, targetRotation) + " : " + turnRate / angle * Time.deltaTime);
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, turnRate / angle * Time.deltaTime);
			}
		}
	}
	//*
	void OnCollisionEnter2D (Collision2D coll) {
		Debug.Log (coll.gameObject);
		turning = true;
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
