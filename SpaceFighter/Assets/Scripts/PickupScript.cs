using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour {
	public float spinSpeed;
	public float timeToDespawn;
	public float warningTime;
	public ParticleSystem particle;



	void Start(){
		StartCoroutine (despawn ());
	}

	void Update () {
		transform.Rotate(0,0,spinSpeed*Time.deltaTime);		
	}

	IEnumerator despawn(){

		yield return new WaitForSeconds(timeToDespawn-warningTime);
		particle.startColor = new Color(1, 0, 0, 1);
		yield return new WaitForSeconds (warningTime);
		Destroy (gameObject);

	}
}
