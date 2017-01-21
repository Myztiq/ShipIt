using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalElement : MonoBehaviour {
	public AudioSource music;
	public float bpm = 156.0F;
	private float nextEventTime;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("SoundBar")) {
			nextEventTime = Mathf.Ceil ((float)AudioSettings.dspTime) - (float)AudioSettings.dspTime;
			print (nextEventTime);
			nextEventTime += 60.0F / bpm;
			print (nextEventTime);
			print (nextEventTime);
			music.PlayScheduled (nextEventTime);
		}
	}
}
