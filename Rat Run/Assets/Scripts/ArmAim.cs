using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAim : MonoBehaviour
{
    public Transform source;
    public Transform crosshair;
    public Transform debugTarget;
    private Vector3 aimDirection;



    
    void Update()
    {
        aimDirection = (crosshair.position - source.position).normalized;
        
        RaycastHit hit;

        if (Physics.Raycast(source.position, aimDirection, out hit, 100f))
        {
            Debug.DrawRay(source.position, aimDirection * hit.distance, Color.green);

            debugTarget.position = hit.point;

        }
    }
}
