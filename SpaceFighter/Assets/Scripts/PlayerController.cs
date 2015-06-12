using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float rotateSpeed;
	public Boundary boundary;

	public GameObject bullet;
	public Transform shotSpawn;
	public float shotsPerSecond;
	public float maxShotsPerSecond;

	private float nextShot;
	private bool playerInvincible = false;

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");


		Vector2 movement = new Vector2 (moveHorizontal, moveVertical); // Skapa rörelsen

		Rigidbody rig = GetComponent<Rigidbody> ();

		rig.AddForce(movement * speed * Time.deltaTime); 

		rig.position = new Vector2( //håll skeppet inom värdena. Mathf.Clamp(vart skeppet är, minimumvärdet, maximumvärdet)
		                           Mathf.Clamp (rig.position.x, boundary.xMin, boundary.xMax), 
		                           Mathf.Clamp(rig.position.y, boundary.yMin, boundary.yMax)
		                           );
		if (rig.position.x == boundary.xMin || rig.position.x == boundary.xMax) {
			rig.velocity = new Vector2 (0, rig.velocity.y);
		}
		if (rig.position.y == boundary.yMin || rig.position.y == boundary.yMax) {
			rig.velocity = new Vector2 (rig.velocity.x, 0);
		}

		
	}
	void Update () {
		Vector2 dir = GetComponent<Rigidbody>().velocity;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime); 

		if (Input.GetButton("Fire1") && Time.time > nextShot) {
			nextShot = Time.time + (1/shotsPerSecond);
			GetComponent<AudioSource>().Play();
			Instantiate (bullet, shotSpawn.position, shotSpawn.rotation);
		}

		
	}

	public bool PlayerInvincible {
		get{return playerInvincible;}
		set{ playerInvincible = value;}
	}

}
