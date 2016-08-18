﻿using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {

	public Rigidbody2D player;

	public float maxStrengthAtDistance = 10.0f;
	public float maxStrength = 100.0f;
	public float twist = -10.0f;
	public float maxScale = 10.0f;
	public float normalScale = 0.7f;
	public float minMagnitude = 0.1f;

	Vector3 originalPosition;
	bool launched;

	// Use this for initialization
	void Start () {
		originalPosition = player.transform.position;
		launched = false;
	}

	// Update is called once per frame
	void Update () {
		if (!launched) {
			// find where mouse hits the world
			Vector3 hitPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// get relative direction
			Vector2 target = hitPoint - transform.position;

			// rotate launcher to point at target
			float angle = Vector2.Angle(Vector2.right, target);
			if (target.y < 0.0f) angle = -angle;
			transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);

			// get launcher scale
			float magnitude = Mathf.Clamp(target.sqrMagnitude / maxStrengthAtDistance, minMagnitude, 1.0f);
			float scale = magnitude * maxScale;
			transform.localScale = new Vector3(scale, normalScale, normalScale);

			// launch?
			if (Input.GetMouseButtonDown(0)) {
				float strength = maxStrength * magnitude;
				Vector2 launchForce = new Vector2(transform.right.x, transform.right.y) * strength;
				player.AddForce(launchForce);
				player.AddTorque(twist);
				launched = true;
			}

		} else {

			if (Input.GetMouseButtonDown(0)) {
				Reset();
			}
		}
	}

	void Reset() {
		player.transform.position = originalPosition;
		player.Sleep();
		launched = false;
	}
}
