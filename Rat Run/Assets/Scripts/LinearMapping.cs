using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RatRun.GameEngine.VR
{
    //Record a gameObject's position across 6DOF
    public class LinearMapping : MonoBehaviour
    {
        public float xValue;
        public float yValue;
        public float zValue;

        public float xAngle;
        public float yAngle;
        public float zAngle;
    }
}

