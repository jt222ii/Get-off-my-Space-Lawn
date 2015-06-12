using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	//spawn
	public GameObject RegularEnemy;
	public GameObject ShootingEnemy;
	public GameObject ZigZagEnemy;
	public Vector3 spawnValues;

	public int hazardCount; 
	public int hazardPlusPerWave;
	public int hazardPlusIncreaseOnDifficultyIncrease;
	public int wavesUntilDiffucultyIncrease;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public int newEnemyInterval;

	//GUITEXT
	public GUIText menuText;
	public GUIText scoreText;
	public GUIText highscoreText;

	//SCORER
	public int scoreByTime;
	public float scoreByTimeInterval;
	private int score;
	private int highScore;

	//misc
	private bool gameOver;
	private bool pauseGame = false;
	private GameObject gameMenu;
	
	private static AudioSource music;

	private Button button;
	void Start()
	{
		if (GameObject.FindWithTag("MusicObject") == null) 
		{
			DontDestroyOnLoad(Instantiate(Resources.Load("Music")));
			music = GameObject.FindWithTag("MusicObject").GetComponent<AudioSource>();
			//Debug.Log(music);
		}
		if (!music.isPlaying) {
			music.Play();
		}
		Time.timeScale = 1.0f;
		highScore = PlayerPrefs.GetInt("highscore");
		UpdateHighscore ();

		if (AudioListener.volume == 0.0f) {
			button = GameObject.FindWithTag ("SoundButton").GetComponent<Button> ();
			button.image.overrideSprite = Resources.Load<Sprite> ("unmuteSoundButton");
		}

		if (music.mute) {
			button = GameObject.FindWithTag ("MusicButton").GetComponent<Button> ();
			button.image.overrideSprite = Resources.Load<Sprite> ("unmuteSoundButton");
		}

		gameMenu = GameObject.Find ("Pause Menu");
		gameMenu.SetActive(false);
		InvokeRepeating ("AddScoreByTime", 1.0f, scoreByTimeInterval);
		gameOver = false;
		menuText.text = "";
		score = 0;
		UpdateScore ();	
		StartCoroutine(SpawnWaves ());
	}


	void Update(){


		if (Input.GetKeyDown (KeyCode.R)) {
			reloadLevel();
		}
		if (Input.GetKeyDown (KeyCode.P) && gameOver == false) {
			pauseGame = !pauseGame;
			if (pauseGame == true) {
				menuText.text = "Game Paused";
				Time.timeScale = 0.0f;
				pauseGame = true;
				music.volume = music.volume * 0.3f;
				GameObject.FindWithTag ("PlayerTurret").GetComponent<TurretScript> ().enabled = false;
				GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().enabled = false;
			}
			if (pauseGame == false) {
				Resume();
			}
			if (pauseGame) {
				gameMenu.SetActive(true);
			} else {
				gameMenu.SetActive(false);
			}
		}
	}
	public void Resume()
	{
		Time.timeScale = 1.0f;
		pauseGame = false;
		music.volume = music.volume / 0.3f;
		GameObject.FindWithTag ("PlayerTurret").GetComponent<TurretScript> ().enabled = true;
		GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().enabled = true;
		gameMenu.SetActive(false);
	}
	public void reloadLevel()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel(Application.loadedLevel);
	}

	
	IEnumerator SpawnWaves()
	{
		int waveCounter = 1;
		yield return new WaitForSeconds (startWait);
		while (true) 
		{
			Debug.Log(hazardCount);
			if(gameOver)
			{
				break;
			}

			for (int i = 1; i <= hazardCount; i++) 
			{
				if(gameOver)
				{
					break;
				}
				Vector3 spawnPosition;

				int randomSpawn = Random.Range(0,4);
				if(randomSpawn == 0)
				{
					//Debug.Log("Uppe");
					spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				}
				else if(randomSpawn == 1)
				{
					//Debug.Log("Vänster");
					spawnPosition = new Vector3 (spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
				}
				else if(randomSpawn == 2)
				{
					//Debug.Log("Höger");
					spawnPosition = new Vector3 ((-spawnValues.x+1), Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
				}
				else
				{
					//Debug.Log("Nere");
					spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), -spawnValues.y, spawnValues.z);
				}

				Quaternion spawnRotation = Quaternion.identity;
				if(i%5==0 && score > newEnemyInterval)
				{
					Instantiate (ShootingEnemy, spawnPosition, spawnRotation);
				}
				else
				{
					if(score > newEnemyInterval*10 && i%2==0)
					{
						Instantiate (ZigZagEnemy, spawnPosition, spawnRotation);
					}
					else if(score > newEnemyInterval*2 && i%4==0)
					{
						Instantiate (ZigZagEnemy, spawnPosition, spawnRotation);
					}
					else
					{
						Instantiate (RegularEnemy, spawnPosition, spawnRotation);
					}
				}
				yield return new WaitForSeconds (spawnWait);
			}


			waveCounter++;
			if(waveCounter%wavesUntilDiffucultyIncrease == 0)
			{
				hazardPlusPerWave += hazardPlusIncreaseOnDifficultyIncrease;
			}
			hazardCount = hazardCount + hazardPlusPerWave;

			yield return new WaitForSeconds(waveWait);
		}
		
	}

	public void GameOver ()
	{
		menuText.text = "GAME OVER\nSCORE: "+score;
		if (score > highScore) {		
			menuText.text = "GAME OVER\nNEW HIGHSCORE: " + score;
			SaveHighScore (score);
		}
		CancelInvoke("AddScoreByTime");
		gameOver = true;
		gameMenu.SetActive (true);
		Button resume = GameObject.FindWithTag("ResumeButton").GetComponent<Button>();
		resume.interactable = false;

	}
	public void AddScoreByTime ()
	{
		AddScore (scoreByTime);
	}
	public void AddScore (int newScoreValue)
	{
		if (!gameOver) {
			score += newScoreValue;
			UpdateScore ();
		}
	}	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	void UpdateHighscore()
	{
		highscoreText.text = "Highscore: " + highScore;
	}

	void SaveHighScore(int newHighscore)
	{
		int oldHighscore = PlayerPrefs.GetInt("highscore", 0);
		if (newHighscore > oldHighscore) {
			PlayerPrefs.SetInt("highscore", newHighscore);
			PlayerPrefs.Save();
			highScore = newHighscore;
			UpdateHighscore();
		}
	}

	public void ExitGame()
	{
		music.volume = music.volume / 0.3f;
		music.Stop ();
		Application.LoadLevel ("StartScene");
	}


	
}
