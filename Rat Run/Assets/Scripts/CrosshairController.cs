using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public Rigidbody controller;
    public Transform crosshair;
    
    
    [Tooltip("Offset of the crosshair from the controller")]
    public float crosshairOffsetX, crosshairOffsetY;

    private void Update()
    {
        crosshair.position = new Vector3(controller.position.x + crosshairOffsetX, controller.position.y + crosshairOffsetY, crosshair.position.z);
    }
}
