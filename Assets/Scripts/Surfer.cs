using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Surfer : MonoBehaviour {
	public float fallSpeed = 0f;
	public float maxFallSpeed = -6f;
	public float turnRateLine = 90f;
	private Quaternion targetRotation;
	private int grounded = 0;
	private bool turning = false;
	private bool jumping = false;
	private bool canjump = false;
	public Train train;
<<<<<<< HEAD
	
=======
    private Button pauseButton;
    private bool paused;
    private UIManager uimanager;

    public int currentSpeed = 0;
>>>>>>> origin/HEAD
	public float[] speeds = new float[6] { 4f, 2f, 3f, 4f, 5f, 6f };
	public float[] maxFallSpeeds = new float[6];
	public float[] gravities = new float[6] { 9.81f, 9.81f, 9.81f, 9.81f, 9.81f, 9.81f };
	public float[] turnRatesJump = new float[6] { 360.0f, 360.0f, 360.0f, 360.0f, 360.0f, 360.0f };

	private float startX;

	void Awake () {
		startX = transform.position.x;
	}

	void Update () {
		if (Input.GetButtonDown ("Jump") && canjump && !jumping) {
			GetComponent<Animator> ().Play ("jump");
			fallSpeed = speeds[Train.level] * 1.5f;
			grounded = 0;
			jumping = true;
			turning = false;
			canjump = false;
		}
<<<<<<< HEAD
		if (jumping) {
			if (Input.GetButton ("Jump")) {
				transform.RotateAround (GetComponent<Collider2D> ().bounds.center, Vector3.back, -turnRatesJump[Train.level] * Time.deltaTime);
=======
        /* Inputs mobile*/
        MobileJump();
        if (jumping) {
			if (Input.GetButton ("Jump") || Input.touchCount > 0) {
				transform.RotateAround (GetComponent<Collider2D> ().bounds.center, Vector3.back, -turnRatesJump[currentSpeed] * Time.deltaTime);
>>>>>>> origin/HEAD
			} else {
				float angle = Quaternion.Angle (transform.rotation, Quaternion.identity);
				if (angle > 2.0f)
					transform.RotateAround (GetComponent<Collider2D> ().bounds.center, Vector3.back, (transform.right.y > 0 ? 1f : -1f) * turnRatesJump[Train.level] * Time.deltaTime);
			}
		}
		if (grounded > 0) {
			fallSpeed = Mathf.Max (0f, fallSpeed);
		} else {
			fallSpeed -= gravities[Train.level] * Time.deltaTime;
			fallSpeed = Mathf.Max (maxFallSpeed, fallSpeed);
		}
		transform.position += new Vector3 (0f, grounded > 0 ? 0f : fallSpeed * Time.deltaTime);
		transform.position = new Vector3 (startX, transform.position.y);
		if (grounded > 0 || turning) {
			float angle = Quaternion.Angle (transform.rotation, targetRotation);
			if (angle <= 5.0f) {
				turning = false;
				canjump = true;
			} else {
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, turnRateLine / angle * Time.deltaTime);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (fallSpeed < 0 || grounded <= 0) {
			if (jumping && !turning) {
				bool success = Vector3.Angle (transform.right, coll.transform.up) < 90;
				train.Trick (success);
				if (!success)
					GetComponent<Animator> ().Play ("ouch");
			}
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
	}

    public void MobileJump()
    {
        if (Input.touchCount > 0 && canjump && !jumping)
        {
            if (EventSystem.current.lastSelectedGameObject == GameObject.Find("PauseRestartInput") ||
                EventSystem.current.currentSelectedGameObject == GameObject.Find("PauseRestartInput"))
            {
                return;
            }
            GetComponent<Animator>().Play("jump");
            fallSpeed = speeds[currentSpeed] * 1.5f;
            grounded = 0;
            jumping = true;
            turning = false;
            canjump = false;
        }
    }
}
