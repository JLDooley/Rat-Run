using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_DrawAOE : MonoBehaviour
{
    public Color colour;
    [Range(0,1)]
    public float transparency = 0.5f;
    public float radius;


    private void OnDrawGizmos()
    {
        colour.a = transparency;
        Gizmos.color = colour;
        Gizmos.DrawSphere(gameObject.transform.position, radius);

        colour.a = 1f;
        Gizmos.color = colour;
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }

}
