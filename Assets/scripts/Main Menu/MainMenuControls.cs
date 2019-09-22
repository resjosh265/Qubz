using UnityEngine;
using System.Collections;

public class MainMenuControls : MonoBehaviour {

	private bool controllerUse;

	public GameObject[] menuOptions;
	private int menuInt;

	private bool up, down;

	private GameObject active;

	public Sprite newGameTex, highScoreTex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		print (menuInt);




		if (controllerUse == true) {
			if (Input.GetAxis ("DPadX") == -1 && down != true) {
				down = true;
				menuInt += 1;
				GetComponent<AudioSource> ().Play ();
			}

			if (Input.GetAxis ("DPadX") == 1 && up != true) {
				up = true;
				menuInt -= 1;
				GetComponent<AudioSource> ().Play ();
			}

			if (menuInt == 0) {
				menuOptions [menuInt].GetComponent<NewGame> ().active = true;
				if (Input.GetButtonDown ("A Button")) {
					menuOptions [menuInt].GetComponent<NewGame> ().pressed = true;
				}
			}else
				menuOptions [0].GetComponent<NewGame> ().active = false;

			if (menuInt == 1) {
				menuOptions [menuInt].GetComponent<HighScores> ().active = true;
				if (Input.GetButtonDown ("A Button")) {
					menuOptions [menuInt].GetComponent<HighScores> ().pressed = true;
				}
			}else
				menuOptions [1].GetComponent<HighScores> ().active = false;

			if (menuInt == 2) {
				menuOptions [menuInt].GetComponent<HowToPlay> ().active = true;
				if (Input.GetButtonDown ("A Button")) {
					menuOptions [menuInt].GetComponent<HowToPlay> ().pressed = true;
				}
			}else
				menuOptions [2].GetComponent<HowToPlay> ().active = false;

			if (menuInt == 3) {
				menuOptions [menuInt].GetComponent<QuitGame> ().active = true;
				if (Input.GetButtonDown ("A Button")) {
					menuOptions [menuInt].GetComponent<QuitGame> ().pressed = true;
				}
			}else
				menuOptions [3].GetComponent<QuitGame> ().active = false;



		} else {
				if (Input.GetAxis ("DPadX") != 0f) {
				controllerUse = true;
				menuInt = 0;
			}
		}
	
	}

	void FixedUpdate() {
		if (Input.GetAxis ("DPadX") == 0) {
			down = false;
			up = false;
		}

		if (menuInt == -1) {
			menuInt = 3;
		}

		if (menuInt == 4) {
			menuInt = 0;
		}
	}

	void OnGUI() {
		/*
		if (GUI.Button (new Rect (10, 10, 100, 100), newGameTex.texture)) {

		}
		*/
	}
}
