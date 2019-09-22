using UnityEngine;
using System.Collections;

public class CubeMove : MonoBehaviour {

	public GameObject pivotPoint;
	public float cubeSpeed = 40f;

	private bool turn;
	private float totalRotation = 0f;
	private float spinAmount;
	private float pivotX;
	private float pivotY;

	private bool turned;
	private bool run2;

	public bool mainTurn;
	public GameObject mainScript;

	public bool isGrounded;
	// Use this for initialization
	void Start () {
		pivotX = 0.5f;
		pivotY = -0.5f;
		mainScript = GameObject.Find ("_MAIN OPTIONS");
	}
	
	// Update is called once per frame
	void Update () {
		
		pivotPoint.transform.localPosition = new Vector3 (pivotX, pivotY, 0);

		//pivotPoint.transform.position = new Quaternion (0.5f, 0.5f, 0);
		if (mainScript.GetComponent<CubeSpawn>().turn == true) {
			if (isGrounded == true) {
				totalRotation = 0f;
				turn = true;
				if (turned == false) {
					turned = true;
				} else
					turned = false;
			}
		}

		if (turn == true) {
			//mainScript.GetComponent<CubeSpawn> ().turn = false;
			spinAmount = Mathf.Min(Time.deltaTime * cubeSpeed, 90f - totalRotation);
			this.transform.RotateAround (pivotPoint.transform.position, Vector3.back, spinAmount);
			totalRotation += spinAmount;
			if (totalRotation >= 90f) {
				if (run2 == false) {
					if (turned == true) {
						pivotY += 1;
					} else {
						pivotX -= 1;
						run2 = true;
					}
				} else {
					if (turned == true) {
						pivotY -= 1;
					} else {
						pivotX += 1;
						run2 = false;
					}
				}


				mainScript.GetComponent<CubeSpawn> ().turn = false;

				turn = false;

			}
		}



	}
}
