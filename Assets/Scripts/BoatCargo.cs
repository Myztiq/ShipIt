using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.VR.WSA.WebCam;

public delegate void CargoAddedDelegate(int cargoCount);
public delegate void CargoRemovedDelegate(int cargoCount);
public delegate void CargoEjectedDelegate(int cargoCount);
public delegate void CargoConsumedDelegate();
public delegate void OutOfCargoDelegate();
public delegate void StarvedDelegate();

public class BoatCargo : MonoBehaviour {

	public GameManager gameManager;
    public GameObject cargoTemplate;
	public AudioSource pickupSound;
	public AudioSource collisionSound;

    public float pauseBetweenEjects = 0.5f;

    public int CargoCount { get { return cargos.Count; } }

    public event CargoAddedDelegate CargoAddedEvent;
    public event CargoRemovedDelegate CargoRemovedEvent;
    public event CargoEjectedDelegate CargoEjectedEvent;
    public event CargoConsumedDelegate CargoConsumedEvent;
    public event OutOfCargoDelegate OutOfCargoEvent;
    public event StarvedDelegate StarvedEvent;

    GameObject cargoHolder;

    List<GameObject> cargos = new List<GameObject>();
    float lastEjectTime;

	// Use this for initialization
	void Start ()
    {
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
		pickupSound.Play ();

		if (GameObject.Find ("StarvingMusic").GetComponent<AudioSource>().isPlaying) {
			GameObject.Find ("BackgroundMusic").GetComponent<AudioSource>().Play();
			GameObject.Find ("StarvingMusic").GetComponent<AudioSource>().Stop();
		}

        SpawnCargoAtIndex (CargoCount);
    }

    public void DropCargo(Vector3 directionToEject, float velocity)
    {
        Debug.Log ("BoatCargo: drop cargo");
        if (CargoCount == 0) {
            return;
        }

        if (ShouldDropCargo()) {
			collisionSound.Play ();
            RemoveCargo ();
            EjectCargo (directionToEject, velocity);
        }

    }

    public void ConsumeCargo()
    {
        Debug.Log ("BoatCargo: consume cargo");
        if (CargoConsumedEvent != null) {
            CargoConsumedEvent ();
        }

        if (!RemoveCargo ()) {
            if (StarvedEvent != null) {
                StarvedEvent ();
            }
        }
    }

    bool RemoveCargo ()
    {
        if (CargoCount == 0) {
            if (OutOfCargoEvent != null) {
                OutOfCargoEvent ();
            }
            return false;
        }

        var removedCargo = cargos [CargoCount - 1];
        cargos.RemoveAt (CargoCount - 1);
        Destroy (removedCargo);

        if (CargoRemovedEvent != null) {
            CargoRemovedEvent (CargoCount);
        }

		if (CargoCount == 0 && OutOfCargoEvent != null) {
			GameObject.Find ("BackgroundMusic").GetComponent<AudioSource>().Stop();
			GameObject.Find ("StarvingMusic").GetComponent<AudioSource>().Play();
            OutOfCargoEvent ();
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

        lastEjectTime = Time.time;

        if (CargoEjectedEvent != null) {
            CargoEjectedEvent (CargoCount);
        }
	}

    bool ShouldDropCargo ()
    {
        return Time.time - lastEjectTime > pauseBetweenEjects;
    }

    void SpawnCargo (int count)
    {
        for(int i = 0; i < count; i++) {
            SpawnCargoAtIndex (i);
        }
    }

    void SpawnCargoAtIndex (int index)
    {
		var y = index * cargoTemplate.transform.localScale.y * Cargo.SCALE_FACTOR;
        var position = cargoHolder.transform.position + new Vector3 (0, y, 0);

        var newRotation = transform.rotation;
        var euler = newRotation.eulerAngles;
        euler.y = index * 20;
        newRotation.eulerAngles = euler;

		var cargo = Instantiate (cargoTemplate, position, newRotation);
		cargo.GetComponent <Cargo> ().SetState (CargoState.OnBoat);
        cargo.transform.SetParent (cargoHolder.transform);

        cargos.Add (cargo);

        if (CargoAddedEvent != null) {
            CargoAddedEvent (CargoCount);
        }
    }
}
