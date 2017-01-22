using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSounds : MonoBehaviour {

	public AudioSource whaleSound;

	void OnCollisionEnter (Collision colWithOther) {
		var other = colWithOther.gameObject;
		if(other.CompareTag("Boat")) {
			whaleSound.Play();
		}
	}
}
