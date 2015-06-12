using UnityEngine;
using System.Collections;

public class ZigZagEnemyScript : MonoBehaviour {

	public Transform target;
	public float speed;
	public float rotationSpeed = 1.0f;
	
	public float timer;

	private Rigidbody enemy;


	void Start()
	{
		try
		{
			enemy = GetComponent<Rigidbody> ();
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
			
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target.position, step);

		
			timer += Time.deltaTime;
			if(timer<1)
			{
				enemy.velocity = transform.up * ((speed+1)*timer);
			}
			if(timer>=1&&timer<2)
			{
				enemy.velocity = transform.up * -((speed+1)*(timer-1));
			}
			if(timer>2)
			{
				timer=0;
			}
			//transform.position += zigzag*Time.deltaTime; 

		} 
		
	}
}
