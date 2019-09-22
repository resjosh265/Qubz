using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Placement : MonoBehaviour {


	private float moveFB;
	private float moveLR;
	private float verticalVelocity;

	public float moveSpeed;
	private Vector3 movement;

	public Material normal;

	private GameObject greenGO;
	public Material green;
	public Material greenIndicator;
	public bool greenUp;
	public Material red;
	public Material redIndicator;
	public bool redUp;
	private GameObject redGO;

	private float timer;

	public Collider[] boxes;


	private bool firstLoop;

	public float destructionSpeed;

	private List<GameObject> greenChain;

	public Camera cam;
	private Rect posTest;

	// Use this for initialization
	void Start () {
		posTest = new Rect(10, 10, 500, 500);
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public void APressed(GameObject floorTile) {
		if (greenUp == false) {
			if (floorTile.GetComponent<FloorProporites> ().color == "Red") {
				greenUp = true;
				floorTile.GetComponent<Renderer> ().material = green;
				floorTile.GetComponent<FloorProporites> ().color = "Green";
				greenGO = floorTile.gameObject;
				greenGO.GetComponent<FloorProporites> ().indicator.GetComponent<MeshRenderer> ().enabled = true;
				greenGO.GetComponent<FloorProporites> ().indicator.GetComponent<Renderer> ().material = greenIndicator;
				redUp = false;
			} else {
				greenUp = true;

				floorTile.GetComponent<Renderer> ().material = green;
				floorTile.GetComponent<FloorProporites> ().color = "Green";
				greenGO = floorTile.gameObject;
				greenGO.GetComponent<FloorProporites> ().indicator.GetComponent<MeshRenderer> ().enabled = true;
				greenGO.GetComponent<FloorProporites> ().indicator.GetComponent<Renderer> ().material = greenIndicator;
			} 



		} else {
			//if there IS NOT a block on the green selected tile
			if (greenGO.GetComponent<FloorProporites> ().cube == null) {
				ResetBlock (greenGO);
				greenGO = null;
				greenUp = false;
			} else {
				//if there IS a block on the green selected tile
				if (greenGO.GetComponent<FloorProporites> ().cubeColor != null) {
					DestroyBlocks (greenGO);
					ResetBlock (greenGO);
					greenGO = null;
					greenUp = false;
				}
			}
		}


	}

	public void BPressed(GameObject floorTile) {
		if (redUp == false) {
			if (floorTile.GetComponent<FloorProporites> ().color == "Green") {
				redUp = true;
				floorTile.GetComponent<Renderer> ().material = red;
				floorTile.GetComponent<FloorProporites> ().color = "Red";
				redGO = floorTile.gameObject;
				redGO.GetComponent<FloorProporites> ().indicator.GetComponent<MeshRenderer> ().enabled = true;
				redGO.GetComponent<FloorProporites> ().indicator.GetComponent<Renderer> ().material = redIndicator;
				greenUp = false;
			} else {
				redUp = true;
				floorTile.GetComponent<Renderer> ().material = red;
				floorTile.GetComponent<FloorProporites> ().color = "Red";
				redGO = floorTile.gameObject;
				redGO.GetComponent<FloorProporites> ().indicator.GetComponent<MeshRenderer> ().enabled = true;
				redGO.GetComponent<FloorProporites> ().indicator.GetComponent<Renderer> ().material = redIndicator;
			}
		} else {
			if (redGO.GetComponent<FloorProporites> ().cube == null) {
				ResetBlock (redGO);
				redGO = null;
				redUp = false;
			} else {
				//if there IS a block on the green selected tile
				if (redGO.GetComponent<FloorProporites> ().cubeColor != null) {
					DestroyBlocks (redGO);
					ResetBlock (redGO);
					redGO = null;
					redUp = false;
				}
			}
		}
	}


	void GreenOnGreen(Transform indicator) {
		bool firstLoopDone = false;

		greenChain = new List<GameObject>();

		GameObject beginingBlock = indicator.gameObject;
		//Vector3 test = new Vector3 (1, 1, 50);
		boxes = Physics.OverlapSphere (indicator.position, 4);

		//Destroy (beginingBlock);
		//boxes = Physics.OverlapBox(indicator.position, test, Quaternion.identity);

		//destroy the boxes
		for (int b = 0; b < boxes.Length; b++) {
			/*if (boxes [b].tag == "Normal Cube") {
				Destroy (boxes [b].gameObject);
				ScoreSystem.CalculateScore (1, 0, 0, 0, 0);
			}*/
			if (boxes [b].tag == "Green Cube") {
				//add the green cube to the chain list
				greenChain.Add(boxes[b].gameObject);
			}
			/*
			if (boxes [b].tag == "Red Cube") {
				//Destroy (boxes [b].gameObject);
				ScoreSystem.CalculateScore (1, 0, 0, 0, 0);
			}
			*/

			/*
			if (b == boxes.Length - 1) {
				for (int i = 0; i < boxes.Length; i++) {
					if (boxes [i].tag == "Block") {
						boxes [i].GetComponent<FloorProporites> ().color = null;
						boxes [i].GetComponent<FloorProporites> ().cubeColor = null;
						boxes [i].GetComponent<FloorProporites> ().cube = null;
					}
				}


			}
			*/
			if (b == boxes.Length - 1) {
				firstLoopDone = true;
			}
		}

		if (firstLoopDone == true) {
			
			StartCoroutine(DestroyChain ());



			//Code to run for each green in the chain

		}



		greenGO.GetComponent<FloorProporites> ().color = null;
		greenGO.GetComponent<FloorProporites> ().cubeColor = null;
		greenGO.GetComponent<FloorProporites> ().cube = null;
		greenGO.GetComponent<Renderer> ().material = normal;
		greenGO.GetComponent<FloorProporites> ().indicator.GetComponent<MeshRenderer> ().enabled = false;
		greenGO = null;
		greenUp = false;
		//boxes = new Collider[20];

	}


	IEnumerator DestroyChain(){

		for (int c = 0; c < greenChain.Count; c++) {
			CubeSpawn.turnPause = true;

			Collider[] testBox;

			testBox = Physics.OverlapSphere (greenChain[c].transform.position, 4);

			Destroy (greenChain [c].gameObject);
			ScoreSystem.greenNum += 1;
			ScoreSystem.multiplier += 1;

			for (int b = 0; b < testBox.Length; b++) {
				if (testBox [b].tag == "Normal Cube") {
					Destroy (testBox [b].gameObject);
					ScoreSystem.grayNum += 1;
				}
				if (testBox [b].tag == "Green Cube") {

					//If the green block is not currently in the list of green blocks to destroy, add it
					bool noNewGreen = false;

					for (int ba = 0; ba < greenChain.Count; ba++) {
						if (greenChain [ba].gameObject != null) {
							if (testBox [b].transform.position == greenChain [ba].transform.position) {
								noNewGreen = true;
							}
						}
					}

					if (noNewGreen == false) {
						greenChain.Add (testBox [b].gameObject);
					}
				}
				if (testBox [b].tag == "Red Cube") {
					Destroy (testBox [b].gameObject);
					ScoreSystem.grayNum += 1;
				}
				if (b == testBox.Length - 1) {
					for (int i = 0; i < testBox.Length; i++) {
						if (testBox [i].tag == "Block") {
							testBox [i].GetComponent<FloorProporites> ().color = null;
							testBox [i].GetComponent<FloorProporites> ().cubeColor = null;
							testBox [i].GetComponent<FloorProporites> ().cube = null;
						}
					}


				}
			}
			yield return new WaitForSeconds (destructionSpeed);

			if (c == greenChain.Count - 1) {
				CubeSpawn.turnPause = false;
				greenChain.Clear ();
				ScoreSystem.CalculateScore ();
			}
		}
	}

	void RedOnRed(Transform indicator) {
		Vector3 test = new Vector3 (1, 1, 50);
		//boxes = Physics.OverlapSphere (indicator.position, 4);
		boxes = Physics.OverlapBox(indicator.position, test, Quaternion.identity);

		ScoreSystem.multiplier += 1;

		for (int b = 0; b < boxes.Length; b++) {
			if (boxes [b].tag == "Normal Cube") {
				Destroy (boxes [b].gameObject);
				ScoreSystem.grayNum += 1;
			}
			if (boxes [b].tag == "Green Cube") {
				Destroy (boxes [b].gameObject);
				ScoreSystem.grayNum += 1;
			}
			if (boxes [b].tag == "Red Cube") {
				Destroy (boxes [b].gameObject);
				ScoreSystem.redNum += 1;
			}
			if (b == boxes.Length - 1) {
				for (int i = 0; i < boxes.Length; i++) {
					if (boxes [i].tag == "Block") {
						boxes [i].GetComponent<FloorProporites> ().color = null;
						boxes [i].GetComponent<FloorProporites> ().cubeColor = null;
						boxes [i].GetComponent<FloorProporites> ().cube = null;
					}
				}
			}
			if (b == boxes.Length - 1) {
				ScoreSystem.CalculateScore ();
			}
		}

		redGO.GetComponent<FloorProporites> ().color = null;
		redGO.GetComponent<FloorProporites> ().cubeColor = null;
		redGO.GetComponent<FloorProporites> ().cube = null;
		redGO.GetComponent<Renderer> ().material = normal;
		redGO.GetComponent<FloorProporites> ().indicator.GetComponent<MeshRenderer> ().enabled = false;
		redGO = null;
		redUp = false;

	}

	void ResetBlock(GameObject selectedSquare) {
		FloorProporites square = selectedSquare.GetComponent<FloorProporites> ();

		square.color = null;
		square.cubeColor = null;
		square.cube = null;
		selectedSquare.GetComponent<Renderer> ().material = normal;
		square.indicator.GetComponent<MeshRenderer> ().enabled = false;
	}

	void DestroyBlocks(GameObject selectedSquare) {
		
		FloorProporites square = selectedSquare.GetComponent<FloorProporites> ();

		if (square.cubeColor == "Normal") {
			//PLACE GUI MESSAGE ON CUBE THAT GETS DESTROYED
			//Vector3 cubePos = cam.WorldToScreenPoint (selectedSquare.GetComponent<FloorProporites> ().cube.transform.position);
			//posTest.x = cubePos.x;
			//posTest.y = cubePos.y;
			Destroy (selectedSquare.GetComponent<FloorProporites> ().cube);
			ScoreSystem.grayNum += 1;
			ScoreSystem.multiplier += 1;
			ScoreSystem.CalculateScore ();




		}

		if (square.cubeColor == "Black") {
			square.color = null;
			selectedSquare.GetComponent<Renderer> ().material = normal;
			square.indicator.GetComponent<MeshRenderer> ().enabled = false;
		}

		if (square.color == "Green") {
			if (square.cubeColor == "Green") {
				GreenOnGreen (selectedSquare.transform);
			}
			if (square.cubeColor == "Red") {
				//CODE IN WHAT HAPPENDS
			}
		}

		if (square.color == "Red") {
			if (square.cubeColor == "Red") {
				RedOnRed (selectedSquare.transform);
			}
			if (square.cubeColor == "Green") {
				//CODE IN WHAT HAPPENDS
			}
		}
	}

	void OnGUI() {
		
		//GUI.Label (posTest, "test");
	}
		
}
