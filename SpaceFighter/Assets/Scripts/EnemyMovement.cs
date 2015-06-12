using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Transform target;
	public float speed;
	public float rotationSpeed = 1.0f;
	
	
	void Start()
	{
		try
		{
		target = GameObject.FindWithTag("Player").transform;
		}
		catch
		{
			Debug.Log("Can't find player. Maybe player is dead!");
			Destroy(gameObject);
		}
		//transform.rotation = Quaternion.LookRotation(target.position - transform.position);
	}
	
	void FixedUpdate()
	{
		if (target != null) {
			var lookDir = target.position - transform.position;
			float angle = Mathf.Atan2 (lookDir.y, lookDir.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, rotationSpeed * Time.deltaTime);
			
			GetComponent<Rigidbody> ().velocity = transform.right * speed;
		} 

	}
}
