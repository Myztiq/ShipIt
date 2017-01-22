using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour {
	private WaveTracker wt;

	void Start(){
		wt = GameObject.Find ("WaveTracker").GetComponent<WaveTracker>();
	}

    void Update ()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(Input.GetMouseButtonDown(0))
            {
                wt.AddWave(new Vector3(hit.point.x, 0, hit.point.z));
            }
        }
    }
}
