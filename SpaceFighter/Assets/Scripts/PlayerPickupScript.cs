using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPickupScript : MonoBehaviour {
	
	private PlayerController playerController;
	public float fireRatePerPickup;

	public float shieldDuration;
	public GUIText shieldTimer;

	public AudioSource pickupFireRateSound;
	public AudioSource shieldUp;
	public AudioSource shieldDown;

	private SpriteRenderer shieldSprite;
	private Image shieldTimerSprite;

	void Start(){
		shieldTimer.text = "";
		shieldTimerSprite = GameObject.FindWithTag ("ShieldTimter").GetComponent<Image>();
		shieldTimerSprite.enabled = false;
		shieldSprite = GameObject.FindWithTag ("ShieldSprite").GetComponent<SpriteRenderer>();
		shieldSprite.enabled = false;
		playerController = gameObject.GetComponent<PlayerController>();
	}
	void OnTriggerEnter(Collider other) {
		if (gameObject.tag == "Player" && other.tag == "PickupFrate"){
			AddFireRate();
			pickupFireRateSound.Play();
			Destroy(other.gameObject);
		} 	
		if (gameObject.tag == "Player" && other.tag == "PickupShield"){
			if(!playerController.PlayerInvincible)
			{
				StartCoroutine(ShieldPickup());
				shieldUp.Play();
				Debug.Log(playerController.PlayerInvincible + " Player is now invincible");
				Destroy(other.gameObject);
			}
			else{
				Debug.Log("Already affected by shield");
			}
		} 	
	}
	
	public void AddFireRate()
	{
		if (playerController.shotsPerSecond < playerController.maxShotsPerSecond) {
			playerController.shotsPerSecond += fireRatePerPickup;
		}
	}
	
	IEnumerator ShieldPickup()
	{
		shieldSprite.enabled = true;
		shieldTimerSprite.enabled = true;

		playerController.PlayerInvincible = true;
		
		float animationTime = shieldDuration;

		while (animationTime > 0) {
			animationTime -= Time.deltaTime;
			shieldTimerSprite.fillAmount = animationTime/shieldDuration;
			yield return null;
		}

		shieldTimerSprite.fillAmount = 1;
		shieldTimerSprite.enabled = false;
		shieldDown.Play();
		shieldTimer.text = "";
		playerController.PlayerInvincible = false;
		shieldSprite.enabled = false;
		Debug.Log(playerController.PlayerInvincible + " Player is no longer invincible");
	}

}
