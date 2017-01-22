using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cargo : MonoBehaviour {
	public static float SCALE_FACTOR = 0.25f;

	private bool isPickUpable = true;
	private bool isEjecting;

	private Vector3 initialScale;
	private Vector3 initialSize;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		initialScale = transform.localScale;
	}

	void Update(){
		if (isEjecting) {
			rb.isKinematic = false;
			rb.useGravity = true;
		} else {
			rb.isKinematic = true;
			rb.useGravity = false;
		}
	}

	public void Eject(){
        SetState (CargoState.Ejecting);
	}

	public bool CanPickUp(){
		return isPickUpable;
	}

    public void SetState(CargoState state) {
        switch(state) {
		case CargoState.OnBoat:
			isEjecting = false;
			isPickUpable = false;
			ScaleDown ();
            break;
        case CargoState.Ejecting:
            isEjecting = true;
            isPickUpable = false;
            break;
		case CargoState.Floating:
			isEjecting = false;
			isPickUpable = true;
			ScaleUp ();
            break;
        default:
            throw new Exception ("Unexpected CargoState: " + state);
        }
    }

	void ScaleDown ()
	{
		var scaleVect = new Vector3 (SCALE_FACTOR, SCALE_FACTOR, SCALE_FACTOR);
		transform.localScale = initialScale - scaleVect;
	}

	void ScaleUp ()
	{
		transform.localScale = initialScale;
	}
		
	void OnCollisionEnter(Collision colEvent){
		if (colEvent.gameObject.CompareTag("Water") && isEjecting) {			
            SetState (CargoState.Floating);
		}
	}
}

public enum CargoState {
    OnBoat,
    Ejecting,
    Floating
}
