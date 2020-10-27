using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class CalculationFunctions : MonoBehaviour
    {
    /// <summary>
    /// Compares two floating point values and returns true if within the tolerance value threshold of each other.
    ///</summary>
    // https://answers.unity.com/questions/756538/mathfapproximately-with-a-threshold.html
    public static bool FastApproximately(float a, float b, float threshold)
        {
            return ((a - b) < 0 ? (b - a) : (a - b)) <= threshold;
        }

        
    }



