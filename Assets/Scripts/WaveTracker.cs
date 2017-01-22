using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTracker : MonoBehaviour {
	public GameObject prefab;
	private List<GameObject> models;
	private List<Vector3> emanators;
	private List<int> ages;

	void Start() {
		models = new List<GameObject>();
		emanators = new List<Vector3>();
		ages = new List<int>();
	}
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		for(var i = 0; i < ages.Count; i++)
		{	
			models[i].transform.localScale += new Vector3(10f, 10f, -1/10f);
			models[i].GetComponent<Renderer>().material.color -= new Color(0f, 0f, 0f, .025f);
			ages[i]++;
		}
		for(int i = emanators.Count -1; i >= 0; i--)
		{
			if(ages[i] == 200)
			{
				Destroy(models[i]);
				models.RemoveAt(i);
				ages.RemoveAt(i);
				emanators.RemoveAt(i);
			}
		}
	}
	public void AddWave(Vector3 emanator) {
        GameObject model = Instantiate(prefab);
        model.transform.position = emanator;
        model.transform.localScale = new Vector3(100, 100, 100);
        models.Add(model);
		emanators.Add(emanator);
		ages.Add(0);
	}
	public Vector3 GetWave(int i) {
		return emanators[i];
	}
	public int GetAge(int i) {
		return ages[i];
	}
	public int GetNumWaves(){
		return emanators.Count;
	}
}
