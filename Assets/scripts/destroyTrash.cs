using UnityEngine;
using System.Collections;

public class destroyTrash : MonoBehaviour {

	public GameObject deathCam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {

		if (col.tag == "Player") {
			GameObject player = col.gameObject;
			player.GetComponentInChildren<Camera> ().enabled = false;
			deathCam.GetComponent<GameOver> ().gameOver = true;
		}
		Destroy (col.gameObject);
	}
}
