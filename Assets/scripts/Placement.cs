using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Qubz.Core;
using Qubz.Gui;
using Qubz.Enums;
using Qubz.Control;

namespace Qubz.Control{
	public class Placement : MonoBehaviour {
		public Material normalPlacementMaterial;
		public Material greenPlacementMaterial;
		public Material greenIndicatorMaterial;
		public Material redPlacementMaterial;
		public Material redIndicatorMaterial;		
		public float destructionSpeed;
		public int greenDestroyRadius = 4;
		public Vector3 redDestroySize = new Vector3 (1, 1, 50);
		
		private GameObject _redGameObject;
		private GameObject _greenGameObject;
		private Collider[] boxes;
		private bool _isGreenActive;
		private bool _isRedActive;

		public void GreenSquareAction(GameObject floorTile) {
			var floorProperties = floorTile.GetComponent<FloorProperties>();

			if (!_isGreenActive) {
				SetSquare(floorProperties, ColorEnum.Green);
				SetSquareIndicator(floorProperties.indicator, ColorEnum.Green);
			} else {
				//if there IS NOT a block on the green selected tile
				if (_greenGameObject.GetComponent<FloorProperties> ().cube == null) {
					ResetBlock (_greenGameObject, ColorEnum.Green);
				} else {
					//if there IS a block on the green selected tile
					if (_greenGameObject.GetComponent<FloorProperties> ().cubeColor != ColorEnum.None) {
						DestroyBlocks (_greenGameObject);
						ResetBlock (_greenGameObject, ColorEnum.Green);
					}
				}
			}
		}

		public void RedSquareAction(GameObject floorTile) {
			var floorProperties = floorTile.GetComponent<FloorProperties>();

			if (!_isRedActive) {
				SetSquare(floorProperties, ColorEnum.Red);
				SetSquareIndicator(floorProperties.indicator, ColorEnum.Red);
			} else {
				//if there IS NOT a block on the green selected tile
				if (_redGameObject.GetComponent<FloorProperties> ().cube == null) {
					ResetBlock (_redGameObject, ColorEnum.Red);
				} else {
					//if there IS a block on the green selected tile
					if (_redGameObject.GetComponent<FloorProperties> ().cubeColor != ColorEnum.None) {
						DestroyBlocks (_redGameObject);
						ResetBlock (_redGameObject, ColorEnum.Red);
					}
				}
			}
		}

		private void SetSquare(FloorProperties tile, ColorEnum color){
			switch(color){
				case ColorEnum.Green:
					tile.GetComponent<Renderer>().material = greenPlacementMaterial;
					_greenGameObject = tile.gameObject;
					_isGreenActive = true;

					if(tile.color == ColorEnum.Red) _isRedActive = false;
					break;
				case ColorEnum.Red:
					tile.GetComponent<Renderer>().material = redPlacementMaterial;
					_redGameObject = tile.gameObject;
					_isRedActive = true;

					if(tile.color == ColorEnum.Green) _isGreenActive = false;
					break;
			}

			tile.color = color;
		}

		private void SetSquareIndicator(GameObject indicator, ColorEnum color){
			switch(color){
				case ColorEnum.Green:
					indicator.GetComponent<Renderer>().material = greenIndicatorMaterial;
					break;
				case ColorEnum.Red:
					indicator.GetComponent<Renderer>().material = redIndicatorMaterial;
					break;
			}

			indicator.GetComponent<MeshRenderer>().enabled = true;
		}

		private Collider[] GetOverlapingCubes(Vector3 position){
			return Physics.OverlapSphere (position, greenDestroyRadius);
		}

		void GreenOnGreen(Transform indicator) {
			var greenChain = new List<GameObject>();

			boxes = GetOverlapingCubes(indicator.position);

			foreach(var cube in boxes){
				var cubeController = cube.GetComponent<CubeController>();

				if(cubeController == null) continue;

				if(cubeController.cubeColor != ColorEnum.Green) continue;

				greenChain.Add(cube.gameObject);
			}

			StartCoroutine(DestroyGreenChain(greenChain));

			ResetFloorProperties(_greenGameObject, true);
			_greenGameObject = null;
			_isGreenActive = false;
		}

		IEnumerator DestroyGreenChain(List<GameObject> chainList){
			for (int c = 0; c < chainList.Count; c++) {
				CubeSpawn.turnPause = true;

				var overlappingCubes = Physics.OverlapSphere (chainList[c].transform.position, 4);

				Destroy (chainList[c].gameObject);
				ScoreSystem.IncreaseBlockScore(ColorEnum.Green);
				ScoreSystem.multiplier += 1;

				for (int b = 0; b < overlappingCubes.Length; b++) {
					if(overlappingCubes[b].tag.ToLower() == "block"){
						ResetFloorProperties(overlappingCubes[b].gameObject, false);
						continue;
					}

					var cubeController = overlappingCubes[b].GetComponent<CubeController>();
					if(cubeController == null) continue;

					if (cubeController.cubeColor == ColorEnum.Green) {

						//If the green block is not currently in the list of green blocks to destroy, add it
						bool greenCubeExists = false;

						for (int ba = 0; ba < chainList.Count; ba++) {
							if (chainList [ba].gameObject != null) {
								if (overlappingCubes[b].transform.position == chainList[ba].transform.position) {
									greenCubeExists = true;
								}
							}
						}

						if (!greenCubeExists) {
							chainList.Add(overlappingCubes [b].gameObject);
						}
					}

					
					if (cubeController.cubeColor == ColorEnum.Gray || cubeController.cubeColor == ColorEnum.Red) {
						ScoreSystem.IncreaseBlockScore(cubeController.cubeColor);
						Destroy(cubeController.gameObject);
					}
				}
				yield return new WaitForSeconds (destructionSpeed);
			}

			CubeSpawn.turnPause = false;
			ScoreSystem.CalculateScore();
		}

		/*
		IEnumerator DestroyGreenChain(List<GameObject> chainList){
			for (int c = 0; c < chainList.Count; c++) {
				CubeSpawn.turnPause = true;

				var overlappingCubes = Physics.OverlapSphere (chainList[c].transform.position, 4);

				Destroy (chainList[c].gameObject);
				ScoreSystem.IncreaseBlockScore(ColorEnum.Green);
				ScoreSystem.multiplier += 1;

				for (int b = 0; b < overlappingCubes.Length; b++) {
					var cubeController = overlappingCubes[b].GetComponent<CubeController>();
					
					if (overlappingCubes [b].tag == "Green Cube") {

						//If the green block is not currently in the list of green blocks to destroy, add it
						bool noNewGreen = false;

						for (int ba = 0; ba < chainList.Count; ba++) {
							if (chainList [ba].gameObject != null) {
								if (overlappingCubes [b].transform.position == chainList [ba].transform.position) {
									noNewGreen = true;
								}
							}
						}

						if (noNewGreen == false) {
							chainList.Add (overlappingCubes [b].gameObject);
						}
					}
					if (overlappingCubes [b].tag == "Normal Cube") {
						Destroy (overlappingCubes [b].gameObject);
						ScoreSystem.grayNum += 1;
					}

					if (overlappingCubes [b].tag == "Red Cube") {
						Destroy (overlappingCubes [b].gameObject);
						ScoreSystem.grayNum += 1;
					}
					if (b == overlappingCubes.Length - 1) {
						for (int i = 0; i < overlappingCubes.Length; i++) {
							if (overlappingCubes [i].tag == "Block") {
								overlappingCubes [i].GetComponent<FloorProperties> ().color = ColorEnum.None;
								overlappingCubes [i].GetComponent<FloorProperties> ().cubeColor = ColorEnum.None;
								overlappingCubes [i].GetComponent<FloorProperties> ().cube = null;
							}
						}


					}
				}
				yield return new WaitForSeconds (destructionSpeed);

				if (c == chainList.Count - 1) {
					CubeSpawn.turnPause = false;
					chainList.Clear ();
					ScoreSystem.CalculateScore ();
				}
			}
			
		}
		*/

		void RedOnRed(Transform indicator) {

			boxes = Physics.OverlapBox(indicator.position, redDestroySize, Quaternion.identity);

			ScoreSystem.multiplier += 1;

			foreach(var cube in boxes){
				if(cube.tag.ToLower() == "block"){
					ResetFloorProperties(cube.gameObject, false);
					continue;
				}
				
				var cubeController = cube.GetComponent<CubeController>();

				if(cubeController == null) continue;

				ScoreSystem.IncreaseBlockScore(cubeController.cubeColor);
				Destroy(cube.gameObject);
			}

			ResetFloorProperties(_redGameObject, true);
			_redGameObject = null;
			_isRedActive = false;
		}

		private void ResetFloorProperties(GameObject obj, bool isIndicatorBlock){
			var floorProperties = obj.GetComponent<FloorProperties>();

			floorProperties.color = ColorEnum.None;
			floorProperties.cubeColor = ColorEnum.None;
			floorProperties.cube = null;

			if(!isIndicatorBlock){
				obj.GetComponent<Renderer> ().material = normalPlacementMaterial;
				floorProperties.indicator.GetComponent<MeshRenderer>().enabled = false;
			}
		}
		void ResetBlock(GameObject selectedSquare, ColorEnum color) {
			if(selectedSquare == null) return;

			FloorProperties square = selectedSquare.GetComponent<FloorProperties>();

			if(square == null) return;

			square.color = ColorEnum.None;
			square.cubeColor = ColorEnum.None;
			square.cube = null;
			selectedSquare.GetComponent<Renderer> ().material = normalPlacementMaterial;
			square.indicator.GetComponent<MeshRenderer> ().enabled = false;
			selectedSquare = null;

			//switch statement so that more colors can be added
			switch(color){
				case ColorEnum.Green:
					_isGreenActive = false;
					break;
				case ColorEnum.Red:
					_isRedActive = false;
					break;
			}
		}

		void DestroyBlocks(GameObject selectedSquare) {
			FloorProperties square = selectedSquare.GetComponent<FloorProperties> ();

			switch(square.cubeColor){
				case ColorEnum.Gray:
					Destroy (square.cube);
					ScoreSystem.grayNum += 1;
					ScoreSystem.multiplier += 1;
					ScoreSystem.CalculateScore();
					break;
				case ColorEnum.Black:
					square.color = ColorEnum.None;
					selectedSquare.GetComponent<Renderer> ().material = normalPlacementMaterial;
					square.indicator.GetComponent<MeshRenderer> ().enabled = false;
					break;
				case ColorEnum.Green:
					GreenOnGreen (selectedSquare.transform);
					break;
				case ColorEnum.Red:
					RedOnRed (selectedSquare.transform);
					break;
			}
		}
	}
}