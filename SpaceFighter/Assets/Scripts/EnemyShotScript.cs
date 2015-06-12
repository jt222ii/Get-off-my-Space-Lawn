using UnityEngine;
using System.Collections;

public class EnemyShotScript : MonoBehaviour {

	public GameObject bullet;
	public Transform shotSpawn;
	public float shotsPerSecond;

	private float nextShot;

	void Update () {
		if (Time.time > 1/shotsPerSecond) {
			if (Time.time > nextShot) {
				nextShot = Time.time + (1 / shotsPerSecond);
				Instantiate (bullet, shotSpawn.position, shotSpawn.rotation);
			}
		}
	}
}
