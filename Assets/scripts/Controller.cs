using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

	private float moveFB;
	private float moveLR;
	private float verticalVelocity;

	public float moveSpeed;
	private Vector3 movement;

	public Material normal;

	private GameObject greenGO;
	public Material green;
	public bool greenUp;
	public Material red;
	public bool redUp;
	private GameObject redGO;

	private float timer;

	public Collider[] boxes;


	private bool firstLoop;

	public GameObject mainScripts;

	//private GameObject indicator;

	//private FloorProporites props;

	private Vector3 target;


	// Use this for initialization
	void Start () {
	
		mainScripts = GameObject.Find ("_MAIN OPTIONS");
		//props = GetComponent<FloorProporites> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		moveFB = Input.GetAxis("Vertical") * moveSpeed;
		//moveLR = Input.GetAxis("Vertical") * moveSpeed;
		moveLR = Input.GetAxis("Horizontal") * moveSpeed;
		//moveFB = Input.GetAxis("Horizontal") * moveSpeed;

		/*
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			Ray touchRay = GetComponentInChildren<Camera> ().ScreenPointToRay (Input.GetTouch (0).position);

			target.y = transform.position.y;

			print (target);
		}
		*/
		//this.GetComponent<CharacterController> ().Move (target * Time.deltaTime);

		//gravity
		if (GetComponent<CharacterController> ().isGrounded == false) {
			verticalVelocity = Physics.gravity.y + Time.deltaTime;
		} else
			verticalVelocity = 0;
		

		movement = new Vector3 (-moveLR + -moveFB, verticalVelocity, moveLR + -moveFB);

		this.GetComponent<CharacterController> ().Move (movement * Time.deltaTime);



	}

	void FixedUpdate() {
		
	}


	void OnTriggerStay(Collider col)
	{
		
		if (col.GetComponent<FloorProporites> ().cube != null) {
			
			this.GetComponent<CharacterController> ().Move (new Vector3 (1, 0, 0));
		}


		if (col.tag == "Block") {
				if (Input.GetButtonDown ("A Button")) {

				//APressed (col.gameObject);
				mainScripts.GetComponent<Placement>().APressed (col.gameObject);
			}



			if (Input.GetButtonDown("B Button")) {
				mainScripts.GetComponent<Placement>().BPressed (col.gameObject);
			}
		} else
			return;
	}

	/*
	void OnGUI(){
		if (GUI.Button (new Rect (100, Screen.height - 300, 100, 100), "up")) {
			moveFB = 1;
		} 
		if (GUI.Button (new Rect (100, Screen.height - 100, 100, 100), "down")) {
			moveFB = -1;
		} 
		if (GUI.Button (new Rect (0, Screen.height - 200, 100, 100), "left")) {
			moveLR = -1;
		} 
		if (GUI.Button (new Rect (200, Screen.height - 200, 100, 100), "right")) {
			moveLR = 1;
		} 
	}
	*/
}
