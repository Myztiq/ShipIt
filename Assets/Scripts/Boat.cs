using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BoatCollidedDelegate();

public class Boat : MonoBehaviour {
	public float impactStrength = 20f;

    public event BoatCollidedDelegate BoatCollidedEvent;

	private Rigidbody rbody;
	private BoatCargo boatCargo;
	private float maxSpeed = 10f;

	private WaveTracker wt;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody>();
		boatCargo = GetComponent<BoatCargo> ();
		wt = GameObject.Find ("WaveTracker").GetComponent<WaveTracker> ();
	}

    // Update is called once per frame
    void Update () {
        for(int i = 0; i < wt.GetNumWaves(); i++)
        {
            Vector3 origin = wt.GetWave(i);
            int age = wt.GetAge(i);
            CheckWaveIntersection(origin, (float)age);
        }

		SetFacing ();
    }

	void SetFacing ()
	{
		transform.right = -new Vector3(rbody.velocity.x, 0, rbody.velocity.z);
	}

    void CheckWaveIntersection (Vector3 origin, float t) {
        float speed = 1/10f;
        Vector3 offset = origin -Vector3.ProjectOnPlane(rbody.position, new Vector3(0, 1, 0));
        if(Mathf.Abs(offset.magnitude -t*speed) < 1f)
        {
			float mgn = impactStrength/(offset.sqrMagnitude)/2;
            rbody.AddForce(-mgn*offset.normalized, ForceMode.Impulse);
        }
    }

	void OnCollisionEnter (Collision colWithOther) {
		var other = colWithOther.gameObject;
		if(other.CompareTag("Obstacle")) {
			float velocity = colWithOther.relativeVelocity.magnitude;
			boatCargo.DropCargo(other.transform.position, velocity);

            if (BoatCollidedEvent != null) {
                BoatCollidedEvent ();
            }
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag ("Cargo")) {
			if (other.GetComponent<Cargo> ().CanPickUp ()) {
				boatCargo.PickupCargo ();
				Destroy (other.gameObject);
			}            
        }
    }
	void FixedUpdate()
	{
		if(rbody.velocity.magnitude > maxSpeed)
		{
			rbody.velocity = rbody.velocity.normalized * maxSpeed;
		}
	}
}
