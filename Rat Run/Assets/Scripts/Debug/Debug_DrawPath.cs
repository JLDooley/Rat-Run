using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Debug_DrawPath : MonoBehaviour
{
    public Transform[] path;

    public float pointSize = 0.5f;
    private void OnDrawGizmos()
    {
        iTween.DrawPath(path);

        foreach (Transform point in path)
        {
            Gizmos.DrawSphere(point.position, pointSize);
        }
    }
}
