﻿using UnityEngine;
using System.Collections;

public class PauseButtonScript : MonoBehaviour {

	public Texture2D button1; // white (unpaused)
	public Texture2D button2; // red (paused)

	// Use this for initialization
	void Start () {
		guiTexture.texture = button1; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		// press and hold
		if (guiTexture.HitTest(Input.GetTouch(0).position) && Input.GetTouch(0).phase != TouchPhase.Ended)
		{
			// do nothing
		}
		// when you let go
		else if (guiTexture.HitTest(Input.GetTouch(0).position) && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			//guiTexture.texture = button1;
			if(guiTexture.name == "Pause Button" && Time.timeScale == 1) // game is running
			{
				guiTexture.texture = button2;
				Time.timeScale = 0; // pause the game
				// Todo: pop up menu
			}
			else if(guiTexture.name == "Pause Button"/*todo: close menu button*/ && Time.timeScale == 0)// game is paused
			{
				guiTexture.texture = button1;
				Time.timeScale = 1; // unpause the game

			}
		}
	}

	void OnGUI()
	{
		// If game is paused
		if (Time.timeScale == 0)
		{
			// Resume button
			if (GUI.Button (new Rect (10, Screen.height/2-30, 100, 50), "Resume")) {
				Time.timeScale = 1;
			}
			// Settings
			// Exit
			if (GUI.Button (new Rect (10, Screen.height/2+30, 100, 50), "Exit")) {
				Time.timeScale = 1;
				Application.LoadLevel("main");
			}
		}

	}
}