using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject boat;

    private Vector3 offset;

    void Start ()
    {
        offset = transform.position - boat.transform.position;
    }

    // Update is called once per frame, guaranteed to run after all items processed in Update
    void LateUpdate ()
    {
        transform.position = boat.transform.position + offset;
    }
}
