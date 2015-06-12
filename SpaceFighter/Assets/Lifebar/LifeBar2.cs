using UnityEngine;
using System.Collections;

public class LifeBar2 : MonoBehaviour {

	void Update () { 
		float rate = 1 / 5;
		float i = 0;
		while (i < 1)
		{
			i += Time.deltaTime * rate;
			GetComponent<Renderer>().material.SetFloat("_Cutoff", i); 
		}


	}
}
