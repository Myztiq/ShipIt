using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalElement : MonoBehaviour {
	private AudioSource music;
	private WaveTracker waveTracker;

	public float bpm = 159.0F;
	private float initDelay = 2;
	private float totalLoopTime = 10;
	private float gameHeight = 100;
	private float nextEventTime;
	private float soundInterval;

	void Start () {
		waveTracker = GameObject.Find ("WaveTracker").GetComponent<WaveTracker>();

		music = GetComponent <AudioSource> ();
		float timeLeft = ((gameHeight - transform.position.z) / gameHeight) * totalLoopTime + initDelay;
		InvokeRepeating("playSound", timeLeft, totalLoopTime);
	}

	void playSound() {
		// When the next sound should be played
		nextEventTime = (float) AudioSettings.dspTime - (float) AudioSettings.dspTime % (60F / bpm) + (60F / bpm);


		// How long until the next sound
		nextEventTime = nextEventTime - (float) AudioSettings.dspTime;

		waveTracker.AddWave(new Vector3(transform.position.x, 0, transform.position.z));
		music.PlayScheduled (nextEventTime);
	}
}
