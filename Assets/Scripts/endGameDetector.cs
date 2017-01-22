using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndGameDetector : MonoBehaviour {

    public GameManager gameManager;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Boat")) {
            gameManager.EndGameWithWin ();
		}
	}
}
