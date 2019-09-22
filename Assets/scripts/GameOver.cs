using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public bool gameOver;
	public GameObject gameOverSprite;
	public GameObject restartSprite;
	public GameObject exitSprite;
	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver == true) {
			GetComponent<Camera> ().enabled = true;
			gameOverSprite.GetComponent<SpriteRenderer> ().enabled = true;
			restartSprite.GetComponent<SpriteRenderer> ().enabled = true;
			exitSprite.GetComponent<SpriteRenderer> ().enabled = true;
			gameOver = false;
		}
	
	}
}
