using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour {

    public float force = 10f;

    private Rigidbody boatRigidbody;

    void Start()
    {
        boatRigidbody = GetComponent <Rigidbody> ();
    }

    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            boatRigidbody.AddForce (Vector3.forward * force);
        }

        if (Input.GetKeyDown (KeyCode.DownArrow)) {
            boatRigidbody.AddForce (Vector3.back * force);
        }

        if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            boatRigidbody.AddForce (Vector3.left * force);
        }

        if (Input.GetKeyDown (KeyCode.RightArrow)) {
            boatRigidbody.AddForce (Vector3.right * force);
        }
    }
}
