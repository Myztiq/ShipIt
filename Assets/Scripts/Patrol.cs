﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {
	public float rotationSpeed = 10f;

	void Update(){
		transform.Rotate (0, rotationSpeed * Time.deltaTime, 0);
	}
}