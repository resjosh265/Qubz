using UnityEngine;
using System.Collections;

public class FloorProporites : MonoBehaviour {

	public string color;
	public string cubeColor;
	public GameObject cube;
	public GameObject indicator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//cube = null;
		//cubeColor = null;
	}

	void OnTriggerStay (Collider col) {
		if (col.tag == "Normal Cube") {
			cubeColor = "Normal";
			cube = col.gameObject;
		}
		if (col.tag == "Green Cube") {
			cubeColor = "Green";
			cube = col.gameObject;
		}
		if (col.tag == "Red Cube") {
			cubeColor = "Red";
			cube = col.gameObject;
		}
		if (col.tag == "Black Cube") {
			cubeColor = "Black";
			cube = col.gameObject;
		}

	}

	void OnTriggerExit (Collider col) {
		cube = null;
		cubeColor = null;
	}
}
