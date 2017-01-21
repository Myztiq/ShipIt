using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBar : MonoBehaviour {
	public float movesPerMinute = 156;
	public float moveDistance = 3;
	private float timeLeft;

	void Start () {
		timeLeft = Time.deltaTime;
	}

	void LateUpdate ()
	{
		timeLeft -= Time.deltaTime;
		if ( timeLeft < 0 ) {
			transform.position = transform.position + new Vector3 (0, 0, moveDistance);
			timeLeft = 60 / movesPerMinute;

			if (transform.position.z > 50) {
				transform.position = new Vector3 (0, 0, -50);
			}
		}
	}
}
