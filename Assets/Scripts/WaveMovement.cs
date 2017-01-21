using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour {

    public WaveTracker wt;

    void Update ()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("click");
                wt.AddWave(new Vector3(hit.point.x, 0, hit.point.z));
            }
        }
    }
}
