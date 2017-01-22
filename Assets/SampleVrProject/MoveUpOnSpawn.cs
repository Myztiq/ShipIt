using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpOnSpawn : MonoBehaviour {
	public float thrustY = 0;
	private Rigidbody rbody;


	void Start () {
		rbody = GetComponent<Rigidbody> ();	
		var target = this.transform.position;
		target.y = 11;

		rbody.velocity = target - this.transform.position;
	}
	

	void Update () {



		if (this.transform.position.y >= 4) {
			Destroy (this.gameObject);
		}
	}
}
