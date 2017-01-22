using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalElement : MonoBehaviour {
	private AudioSource music;
	public float bpm = 156.0F;
	private float initDelay = 2;
	private float totalLoopTime = 10;
	private float gameHeight = 100;
	private float nextEventTime;
	private float timeLeft;
	private float soundInterval;

	void Start () {
		music = GetComponent <AudioSource> ();
		timeLeft = ((gameHeight - transform.position.z) / gameHeight) * totalLoopTime + initDelay;
	}

	void playSound() {
		nextEventTime = Mathf.Ceil ((float)AudioSettings.dspTime) - (float)AudioSettings.dspTime;
		nextEventTime += 60.0F / bpm;
		WaveTracker.Instance.AddWave(new Vector3(transform.position.x, 0, transform.position.z));
		music.PlayScheduled (nextEventTime);
	}

	void LateUpdate ()
	{
		timeLeft -= Time.deltaTime;
		if ( timeLeft < 0 ) {
			playSound ();
			timeLeft = totalLoopTime;
		}
	}
}
