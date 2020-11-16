using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAim : MonoBehaviour
{
    public Transform source;
    public Transform crosshair;
    public Transform debugTarget;
    public Transform arm;

    private Vector3 aimDirection;

    public LayerMask rayLayerMask;



    
    void Update()
    {
        aimDirection = (crosshair.position - source.position).normalized;
        
        RaycastHit hit;

        if (Physics.Raycast(source.position, aimDirection, out hit, 100f, rayLayerMask))
        {
            Debug.DrawRay(source.position, aimDirection * hit.distance, Color.green);

            debugTarget.position = hit.point;

            //Vector3 targetVector = new Vector3(hit.point.x - arm.position.x, hit.point.y - arm.position.y , hit.point.z - arm.position.z).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(hit.point - arm.position);

            //Quaternion targetRotation = Quaternion.Euler(targetVector);
            arm.rotation = Quaternion.RotateTowards(arm.rotation, targetRotation, 10f);

        }
    }
}
