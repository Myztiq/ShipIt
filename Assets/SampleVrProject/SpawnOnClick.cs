using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnClick : MonoBehaviour {
	public GameObject objectToSpawn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			bool didHit = Physics.Raycast (ray, out hit);

			if (didHit) {
				Vector3 spawnPointOrigin = hit.point;
				spawnPointOrigin.y = 0;

				Quaternion rotation = new Quaternion ();
				Instantiate (objectToSpawn, spawnPointOrigin, rotation);
			}

		}
	}
}
