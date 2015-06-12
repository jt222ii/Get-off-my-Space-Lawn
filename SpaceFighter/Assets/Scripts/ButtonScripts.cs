using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour {

	private Button button;

	public void MuteMusic()
	{
		AudioSource music = GameObject.FindWithTag ("MusicObject").GetComponent<AudioSource> ();
		button = GameObject.FindWithTag("MusicButton").GetComponent<Button>();
		if (music.mute) {
			button.image.overrideSprite = Resources.Load<Sprite>("MuteMusicButton");
			music.mute = false;
		}
		else
		{
			button.image.overrideSprite = Resources.Load<Sprite>("unmuteMusicButton");
			music.mute = true;
		}
	}
	public void MuteSound()
	{
		button = GameObject.FindWithTag("SoundButton").GetComponent<Button>();
	
		if (AudioListener.volume < 1.0F) {
			button.image.overrideSprite = Resources.Load<Sprite>("MuteSoundButton");
			AudioListener.volume = 1.0F;
		}
		else
		{
			button.image.overrideSprite = Resources.Load<Sprite>("unmuteSoundButton");
			AudioListener.volume = 0.0F;
		}
	}


}
