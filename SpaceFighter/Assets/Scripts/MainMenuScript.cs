using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	private GameObject helpInfo;
	public GameObject starEmitter;
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot;
	// Use this for initialization
	void Start () {
		hotSpot = new Vector2 (32, 32);
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		Time.timeScale = 1.0f;
		//starEmitter.GetComponent<ParticleSystem> ().Play();
		helpInfo = GameObject.Find ("howToPlay");
		helpInfo.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void StartGame()
	{
		Application.LoadLevel ("MainScene");
	}
	
	public void ShowHelp()
	{
		helpInfo.SetActive(true);
	}
	public void CloseHelp()
	{
		helpInfo.SetActive(false);
	}
}
