using UnityEngine;
using System.Collections;

public class HowToPlay : MonoBehaviour {

	public Sprite mouseOut;
	public Sprite mouseOver;
	public Texture tutorial;

	private bool screenUp;

	public bool active;
	public bool pressed;

	//change the sprite when mouse enters the collider
	void Update() {
		if (active == true) {
			GetComponent<SpriteRenderer> ().sprite = mouseOver;
		} else
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

		//tutorial.GetComponent<SpriteRenderer> ().enabled = true;


		if (screenUp == false) {
			screenUp = true;
		} else
			screenUp = false;

		pressed = false;

	}


	void OnGUI () {

		if (screenUp == true) {
			float width = Screen.width / 2;
			float height = Screen.height / 1.2f;
			GUI.Box (new Rect ((width - 250), (Screen.height / 2 - 250), 500, 500), tutorial);
		}
	}

}
