using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_DrawOrientation : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        //  x-axis
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);

        //  y-axis
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);

        //  z-axis
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
