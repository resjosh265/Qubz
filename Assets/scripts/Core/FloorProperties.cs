using UnityEngine;
using System.Collections;
using Qubz.Enums;

namespace Qubz.Core{
	public class FloorProperties : MonoBehaviour {

		public ColorEnum color;
		public ColorEnum cubeColor;
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
				cubeColor = ColorEnum.Gray;
				cube = col.gameObject;
			}
			if (col.tag == "Green Cube") {
				cubeColor = ColorEnum.Green;
				cube = col.gameObject;
			}
			if (col.tag == "Red Cube") {
				cubeColor = ColorEnum.Red;
				cube = col.gameObject;
			}
			if (col.tag == "Black Cube") {
				cubeColor = ColorEnum.Black;
				cube = col.gameObject;
			}

		}

		void OnTriggerExit (Collider col) {
			cube = null;
			cubeColor = ColorEnum.None;
		}
	}
}