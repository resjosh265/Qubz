using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Qubz.Core;
using UnityEngine;

namespace Qubz.Movement{
    public class CubeMover : MonoBehaviour
    {
        public GameObject pivotPointObject;
        public GameObject mainScript;

        public List<float> rotations;
        public List<Vector3> pivotPositions;

        public bool turn;

        private int currentRotationIndex;
        private int currentPivotIndex;
        private float currentRotationGoal = -90f;
        private int currentPivotPoint = 0;
        private bool isFreshSpawn = true;
        private float freshSpawnTimer = 3f;
        private bool isKinematic;

        private Vector3 newEndpoint;

        private void Start(){
            //newEndpoint = new Vector3(transform.position.x + 4, transform.position.y, transform.position.z);
            mainScript = GameObject.Find ("_MAIN OPTIONS");
        }

        private void Update() {
            if(isFreshSpawn) {
                FreshSpawnTimerController();
            }else{
                if(!isKinematic) SetSelfKinematic();
                HardSetYPosition();
            }

            if(mainScript.GetComponent<CubeSpawn>().turn && !isFreshSpawn) {
                turn = true;
            }

            if(turn) RotateCube();
        }

        private void FreshSpawnTimerController(){
            if(freshSpawnTimer > 0){
                freshSpawnTimer -= Time.deltaTime;
                return;
            }

            isFreshSpawn = false;
        }

        private void HardSetYPosition(){
            transform.position = new Vector3(transform.position.x, 1.98f, transform.position.z);
        }

        private void SetSelfKinematic(){
            var rigidBody = GetComponent<Rigidbody>();

            rigidBody.isKinematic = true;
            isKinematic = true;
        }

        private void RotateCube(){      
            transform.RotateAround(pivotPointObject.transform.position, Vector3.back, 100 * Time.deltaTime);

            
            if(transform.eulerAngles.z >= rotations[currentRotationIndex] - 1 && transform.eulerAngles.z <= rotations[currentRotationIndex] + 1 ){
                transform.eulerAngles = new Vector3(0, 0, rotations[currentRotationIndex]);
                SetNextRotationGoal();
                SetNextPivotPosition();
                turn = false;

            }

            
        }

        private void SetNextRotationGoal(){
            currentRotationIndex++;

            if(currentRotationIndex > rotations.Count - 1){
                currentRotationIndex = 0;
            }
        }

        private void SetNextPivotPosition(){
            currentPivotIndex++;

            if(currentPivotIndex > pivotPositions.Count - 1){
                currentPivotIndex = 0;
            }

            pivotPointObject.transform.localPosition = pivotPositions[currentPivotIndex];
        }

        private void SetNextPositionGoal(){
            newEndpoint = new Vector3(transform.position.x + 4, transform.position.y, transform.position.z);
        }
    }
}