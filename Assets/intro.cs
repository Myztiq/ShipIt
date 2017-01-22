using UnityEngine;
using System.Collections;

public class SCBlueButton : MonoBehaviour {


	public void  ModeSelect(){
		StartCoroutine("Wait");

	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(2);

		Application.LoadLevel("Lighting");
	}
}