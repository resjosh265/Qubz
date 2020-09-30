using UnityEngine;
using Qubz.Core;
using Qubz.Movement;
using Qubz.Enums;

namespace Qubz.Control{
	public class PlayerController : MonoBehaviour {
		public GameObject mainScripts;
		
		private Vector3 _moveDirection;
		private Mover _mover;
		private Placement _placement;
		private GameObject _currentBlock;

		private void Start () {
			_mover = GetComponent<Mover>();
			_placement = GetComponent<Placement>();
			mainScripts = GameObject.Find ("_MAIN OPTIONS");
		}
		
		private void Update () {
			if(_mover.IsGrounded()){
				MovementController();
			}

			PlacementController();
			
			_moveDirection.y += _mover.ApplyGravity();
			_mover.Move(_moveDirection);
		}

		private void MovementController(){
			var vertical = Input.GetAxis("Vertical");
			var horizontal = Input.GetAxis("Horizontal");

			_moveDirection = new Vector3 (-horizontal + -vertical, 0.0f, horizontal + -vertical);
		}

		private void PlacementController(){
			if(Input.GetButtonDown("A Button")){
				_placement.GetComponent<Placement>().GreenSquareAction(_currentBlock);
			}

			if(Input.GetButtonDown("B Button")){
				_placement.GetComponent<Placement>().RedSquareAction(_currentBlock);
			}
		}

		void OnTriggerEnter(Collider col)
		{
			if(col.tag.ToLower() != "block") return;

			/* pushback player if cube occupies square
			if (col.GetComponent<FloorProporites>().cube != null) {
				
				this.GetComponent<CharacterController> ().Move (new Vector3 (1, 0, 0));
			}
			*/

			_currentBlock = col.gameObject;
		}
	}
}
