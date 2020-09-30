using UnityEngine;

namespace Qubz.Movement{
    public class Mover : MonoBehaviour
    {
        public float moveSpeed = 0.02f;
        private CharacterController _characterController;

        private void Start () {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector3 moveDirection){
            moveDirection *= moveSpeed;

            _characterController.Move(moveDirection);
        }

        public float ApplyGravity()
        {
            return Physics.gravity.y * Time.deltaTime;
        }

        public bool IsGrounded(){
            return _characterController.isGrounded;
        }
    }
}
