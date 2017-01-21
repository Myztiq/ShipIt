using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cargo : MonoBehaviour {
	
	private bool isPickUpable = true;
	private bool isEjecting;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
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
            break;
        case CargoState.Ejecting:
            isEjecting = true;
            isPickUpable = false;
            break;
        case CargoState.Floating:
            isEjecting = false;
            isPickUpable = true;
            break;
        default:
            throw new Exception ("Unexpected CargoState: " + state);
        }
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
