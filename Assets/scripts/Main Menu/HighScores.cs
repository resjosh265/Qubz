﻿using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour {

	public Sprite mouseOut;
	public Sprite mouseOver;

	public bool active;
	public bool pressed;

	//change the sprite when mouse enters the collider
	void Update() {
		if (active == true) {
			GetComponent<SpriteRenderer> ().sprite = mouseOver;
		}else
			GetComponent<SpriteRenderer> ().sprite = mouseOut;

		if (pressed == true) {
			OnMouseDown ();
		}
	}
	//change the sprite when mouse enters the collider
	void OnMouseEnter() {
		active = true;
		GetComponent<AudioSource> ().Play ();
	}

	//change the sprite when the mouse exits the collider
	void OnMouseExit() {
		active = false;
	}

	//Exit the game when the collider is pressed
	void OnMouseDown() {
		
	}
}
