using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeSpawn : MonoBehaviour {

	public Transform[] spawns;
	public GameObject cubeNormal, cubeGreen, cubeRed, cubeBlack;
	public GameObject start;

	public int greenChance;
	public int redChance;
	public int blackChance;

	private float timer;

	public bool turn;
	public float turnTime = 5f;
	private float turnTimer;
	public static bool turnPause;

	public bool isGrounded;
	// Use this for initialization
	void Start () {
	
		for (int i = 0; i < 8; i++) {
			int number = Random.Range (0, 100);
			if (number <= greenChance) {
				Instantiate (cubeGreen, spawns [i].position, Quaternion.identity);
			}
			if (number > greenChance && number <= greenChance + redChance) {
				Instantiate (cubeRed, spawns [i].position, Quaternion.identity);
			}
			if (number > greenChance + redChance && number <= greenChance + redChance + blackChance) {
				Instantiate (cubeBlack, spawns [i].position, Quaternion.identity);
			}
			if (number > greenChance + redChance + blackChance) {
				Instantiate (cubeNormal, spawns [i].position, Quaternion.identity);
			}
		}
		timer = turnTime;

	}
	
	// Update is called once per frame
	void Update () {

		//Controls the timer pause when a chain is in progress
		if (turnPause == false) {
			timer -= Time.deltaTime;
		} else {
			if (timer >= 1) {
				timer -= Time.deltaTime;
			}
		}

		if (timer <= 0) {
			

			for (int i = 0; i < 8; i++) {
				int number = Random.Range (0, 100);
				if (number <= greenChance) {
					Instantiate (cubeGreen, spawns [i].position, Quaternion.identity);
				}
				if (number > greenChance && number <= greenChance + redChance) {
					Instantiate (cubeRed, spawns [i].position, Quaternion.identity);
				}
				if (number > greenChance + redChance && number <= greenChance + redChance + blackChance) {
					Instantiate (cubeBlack, spawns [i].position, Quaternion.identity);
				}
				if (number > greenChance + redChance + blackChance) {
					Instantiate (cubeNormal, spawns [i].position, Quaternion.identity);
				
				}
			}
			turn = true;
			timer = turnTime;
			//turn = false;
			/*
			if (isGrounded == true) {
				turn = true;
				timer = 5;
			}
			*/
		}


		if (turn == true) {
			
				turnTimer += Time.deltaTime;
				if (turnTimer >= 0.03f) {
					turn = false;
					turnTimer = 0;
				}

		}

	
	}
}
