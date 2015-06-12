using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {
	public float speed;
	// Use this for initializationnew 
	void Start () {
		GetComponent<Rigidbody> ().velocity = transform.right * speed; 
	}

}
