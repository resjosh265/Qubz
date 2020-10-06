using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qubz.Enums;
using Qubz.Movement;

namespace Qubz.Control{
    public class CubeController : MonoBehaviour
    {
        public ColorEnum cubeColor;

        public void DisableCubeMover(){
            GetComponent<CubeMover>().enabled = false;
        }
    }
}
