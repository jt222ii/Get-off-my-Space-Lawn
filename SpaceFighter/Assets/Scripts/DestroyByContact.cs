using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	public GameObject explosion;
	public GameObject playerExplosion;

	public GameObject fireRatePickup;
	public GameObject shieldPickup;
	public int numberOfPickups;

	public int pickupDropPercent;
	public int laterPickupDropPercent;
	public int lowerDropRateAtAmountOfEnemies;


	public float explosionTime;
	public int scoreValue;

	private GameController gameController;
	private PlayerController playerController;


	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		GameObject playerControllerObject = GameObject.FindWithTag ("Player");
		try
		{
			playerController = playerControllerObject.GetComponent<PlayerController>();

		}
		catch
		{
			Debug.Log("Player is dead");
		}
		if (gameControllerObject != null)  {
			gameController = gameControllerObject.GetComponent<GameController>();
			if (gameController.hazardCount > lowerDropRateAtAmountOfEnemies) {
				pickupDropPercent = laterPickupDropPercent;
			}
		}
		else 
		{
			Debug.Log("Can not find GameController and/or PlayerController script!");
		}


	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "EnemyShot" || other.tag == "PickupFrate" || other.tag == "PickupShield") {
			return;
		} else if (other.tag == "Player" && playerController.PlayerInvincible == false) {
			GameObject clonePlayerExplosion = (GameObject)Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			Destroy (clonePlayerExplosion, (explosionTime - 1));
			gameController.GameOver ();
			Destroy (other.gameObject);
		} else if (other.tag == "PlayerShot") {
			gameController.AddScore (scoreValue);
			Destroy(other.gameObject);
		}
		else{
			gameController.AddScore (scoreValue);
		} 

		if (gameObject.tag != "EnemyShot") {
			int randomChance = Random.Range (0, 100);
			if(randomChance > 0 && randomChance <= pickupDropPercent)
			{
				int randomPickup = Random.Range (0, numberOfPickups+1);
				if (randomPickup == 0) {
					Instantiate (shieldPickup, transform.position, transform.rotation);
				}
				else if(playerController.shotsPerSecond < playerController.maxShotsPerSecond)
				{
					if (randomPickup == numberOfPickups-1 ||randomPickup == numberOfPickups) {
						Instantiate (fireRatePickup, transform.position, transform.rotation);
					}
				}
			}
		}
		Destroy (gameObject);
		GameObject cloneExplosion = (GameObject)Instantiate (explosion, transform.position, transform.rotation);
		Destroy (cloneExplosion, explosionTime);
	}
}
