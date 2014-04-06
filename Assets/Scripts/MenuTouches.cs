﻿using UnityEngine;
using System.Collections;

public class MenuTouches : MonoBehaviour {
	
	public Texture2D button1;
	public Texture2D button2;
	public GUIText loading;
	public AudioClip menuButton;
	private float effectsVolume = 0.0F;
	
	void Start(){
		loading.enabled = false;
		
		if (PlayerPrefs.HasKey ("effectsVolume")) {
			effectsVolume = PlayerPrefs.GetInt ("effectsVolume");
		}
		AudioListener.volume = effectsVolume / 10;
	}
	
	void OnMouseUp(){
		if (this.name == "text_START"){
			Destroy(GameObject.Find ("Music")); // Stop menu music
			StartCoroutine(LoadMainHall());
			StartCoroutine(Loading());
		}
		else if (this.name == "text_Settings"){
			StartCoroutine(LoadSettings ());
		}
		else if (this.name == "text_Back"){
			StartCoroutine(LoadMainMenu ());
		}
		else if (this.name == "text_ClearHighscore"){
			PlayerPrefs.DeleteKey ("highScore");
			guiTexture.texture = button1;
			audio.PlayOneShot(menuButton);
		}
		else if (this.name == "text_Backtomenu"){
			StartCoroutine(LoadMainMenu ());
		}
		else if (this.name == "text_Help"){
			StartCoroutine(LoadHelp1 ());
		}
		else if (this.name == "Left_Button"){ // back (help screen)
			if(Application.loadedLevelName == "HelpScreen2"){
				StartCoroutine(LoadHelp1 ());
			}
			else if(Application.loadedLevelName == "HelpScreen3"){
				StartCoroutine(LoadHelp2 ());
			}
			else if(Application.loadedLevelName == "HelpScreen4"){
				StartCoroutine(LoadHelp3 ());
			}
		}
		else if (this.name == "Right_Button"){ // forward (help screen)
			if(Application.loadedLevelName == "HelpScreen1"){
				StartCoroutine(LoadHelp2 ());
			}
			else if(Application.loadedLevelName == "HelpScreen2"){
				StartCoroutine(LoadHelp3 ());
			}
			else if(Application.loadedLevelName == "HelpScreen3"){
				StartCoroutine(LoadHelp4 ());
			}
		}
	}
	
	void OnMouseDown(){
		if (guiTexture.name == "text_START")
			guiTexture.texture = button2;
		else if (guiTexture.name == "text_Settings")
			guiTexture.texture = button2;
		else if (guiTexture.name == "text_Back")
			guiTexture.texture = button2;
		else if (guiTexture.name == "text_ClearHighscore"){
			guiTexture.texture = button2;
		}
		else if (guiTexture.name == "text_Backtomenu"){
			guiTexture.texture = button2;
		}
		else if (guiTexture.name == "text_Help"){
			guiTexture.texture = button2;
		}
		else if (guiTexture.name == "Left_Button"){
			guiTexture.texture = button2;
		}
		else if (guiTexture.name == "Right_Button"){
			guiTexture.texture = button2;
		}
	}
	
	IEnumerator Loading(){
		loading.enabled = true;
		yield break;
	}
	IEnumerator LoadSettings(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("settings");
		yield break;
	}
	
	IEnumerator LoadMainMenu(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("mainMenu");
		yield break;
	}
	
	IEnumerator LoadMainHall(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("MainHall");
		yield break;
	}
	
	IEnumerator LoadHelp1(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen1");
		yield break;
	}
	
	IEnumerator LoadHelp2(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen2");
		yield break;
	}
	IEnumerator LoadHelp3(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen3");
		yield break;
	}
	IEnumerator LoadHelp4(){
		audio.PlayOneShot(menuButton);
		guiTexture.texture = button1;
		yield return new WaitForSeconds(0.2F);
		Application.LoadLevel ("HelpScreen4");
		yield break;
	}
}

	// Update is called once per frame
	//void Update () 
	//{
		/* Notes: Both methods seem to work.
		 * Theory on how to add pen touch recognition: Use method 2, then check if isPenPressed == true.
		 * If it is true, getPenTouchX() and getPenTouchY(). 
		 * Make a new Vector3: Vector3 position = new Vector3(getPenTouchX(), getPenTouchY(), 0);
		 * Put those values into HitTest(Vector3 position)
		 * Pray that it works.
		 */

		// Method 1 (possible multi-touch functionality, has bugs)
		/*foreach (Touch touch in Input.touches)
		{
			if (guiTexture.HitTest(touch.position) && touch.phase != TouchPhase.Ended)
			{
				guiTexture.texture = button2;
				//transform.Translate(Vector3.right * 1 * Time.smoothDeltaTime);

			}
			else if (guiTexture.HitTest(touch.position) && touch.phase == TouchPhase.Ended)
			{
				guiTexture.texture = button1;
				if(guiTexture.name == "text_START")
					Application.LoadLevel("game");
				else if(guiTexture.name == "text_Settings")
					Application.LoadLevel("settings");
				else if(guiTexture.name == "text_Back")
					Application.LoadLevel("main");
			}
		}*/

		// Method 2
		/*if (guiTexture.HitTest(Input.GetTouch(0).position) && Input.GetTouch(0).phase != TouchPhase.Ended)
		{
			guiTexture.texture = button2; // texture changes to button2 when touched
			//transform.Translate(Vector3.right * 1 * Time.smoothDeltaTime);
			
		}
		else if (guiTexture.HitTest(Input.GetTouch(0).position) && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			guiTexture.texture = button1;
			if(guiTexture.name == "text_START")
				Application.LoadLevel("game");
			else if(guiTexture.name == "text_Settings")
				Application.LoadLevel("settings");
			else if(guiTexture.name == "text_Back")
				Application.LoadLevel("main");
			else if(guiTexture.name == "text_ClearHighscore")
				PlayerPrefs.DeleteKey ("highScore");
		}
	}*/

