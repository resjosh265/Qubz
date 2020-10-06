using UnityEngine;
using Qubz.Core;
using Qubz.Control;

public class KinimaticOn : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		var cubeController = col.GetComponent<CubeController>();
		
		if(cubeController == null) return;
		
		Rigidbody rigidBody = col.gameObject.GetComponent<Rigidbody>();
		print(rigidBody);
		rigidBody.isKinematic = false;
		cubeController.DisableCubeMover();
			
			
		//make the blocks kinematic when they reach the ground so physics do not apply
		/*
		if (col.tag == "Black Cube" | col.tag == "Red Cube" | col.tag == "Green Cube" | col.tag == "Normal Cube") {
			
			Rigidbody rb = col.GetComponent<Rigidbody> () as Rigidbody;
			CubeMove cm = col.GetComponent<CubeMove> () as CubeMove;

			if (rb.isKinematic == false) {
				rb.isKinematic = true;
				cm.isGrounded = true;
			} else
				rb.isKinematic = false;
		}
		*/
	}
}
