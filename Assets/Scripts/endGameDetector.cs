using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class endGameDetector : MonoBehaviour {

	public Text winText;
	public GameManager gameManager;

	void Start () {
		winText.text = "";
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Boat")) {
			other.gameObject.SetActive(false);
			winText.text = "You Win! Points: " + gameManager.PointsString() ;
		}
	}
}
