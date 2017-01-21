using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.WebCam;

public delegate void CargoAddedDelegate(int cargoCount);
public delegate void CargoRemovedDelegate(int cargoCount);
public delegate void StarvedDelegate();

public class BoatCargo : MonoBehaviour {

	public GameManager gameManager;
    public GameObject cargoTemplate;
    public int CargoCount { get { return cargos.Count; } }

    public event CargoAddedDelegate CargoAddedEvent;
    public event CargoRemovedDelegate CargoRemovedEvent;
    public event StarvedDelegate StarvedEvent;

    GameObject cargoHolder;

    Vector3 cargoSize;
    List<GameObject> cargos = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        cargoSize = cargoTemplate.GetComponent <MeshRenderer> ().bounds.size;
		cargoHolder = GameObject.Find ("Cargo Holder");
		SpawnCargo (gameManager.startingCargoCount);
	}

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.C)) {
            if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
				DropCargo (transform.position, 1f);
            }
            else {
                PickupCargo ();
            }
        }
    }

    public void PickupCargo()
    {
        Debug.Log ("BoatCargo: pickup cargo");
        SpawnCargoAtIndex (CargoCount);
    }

    public void DropCargo(Vector3 directionToEject, float velocity)
    {
        Debug.Log ("BoatCargo: drop cargo");
        if (CargoCount == 0) {
            return;
        }

        RemoveCargo ();
        EjectCargo (directionToEject, velocity);
    }

    public void ConsumeCargo()
    {
        Debug.Log ("BoatCargo: consume cargo");
        if (!RemoveCargo ()) {
            if (StarvedEvent != null) {
                StarvedEvent ();
            }
        }
    }

    bool RemoveCargo ()
    {
        if (CargoCount == 0) {
            return false;
        }

        var removedCargo = cargos [CargoCount - 1];
        cargos.RemoveAt (CargoCount - 1);
        Destroy (removedCargo);

        if (CargoRemovedEvent != null) {
            CargoRemovedEvent (CargoCount);
        }

        return true;
    }

	void EjectCargo (Vector3 directionToEject, float velocity)
	{
		var pos = cargoHolder.transform.position;

		var cargo = Instantiate (cargoTemplate, pos, transform.rotation);
		cargo.GetComponent<Cargo> ().Eject ();

		var rb = cargo.GetComponent<Rigidbody> ();
		rb.AddForce ((pos - directionToEject) * velocity, ForceMode.Impulse);
	}

    void SpawnCargo (int count)
    {
        for(int i = 0; i < count; i++) {
            SpawnCargoAtIndex (i);
        }
    }

    void SpawnCargoAtIndex (int index)
    {
        var y = index * cargoSize.y;
        var position = cargoHolder.transform.position + new Vector3 (0, y, 0);

        var newRotation = transform.rotation;
        var euler = newRotation.eulerAngles;
        euler.y = index * 20;
        newRotation.eulerAngles = euler;

        var cargo = Instantiate (cargoTemplate, position, newRotation);
        cargo.transform.SetParent (cargoHolder.transform);
        cargo.GetComponent <Cargo> ().SetState (CargoState.OnBoat);

        cargos.Add (cargo);

        if (CargoAddedEvent != null) {
            CargoAddedEvent (CargoCount);
        }
    }
}
