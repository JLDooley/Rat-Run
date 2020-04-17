using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public Rigidbody controller;
    public Rigidbody arm;
    public float xOff;
    public float yOff;
    public float zOff;
    


    void FixedUpdate()
    {
        
        arm.MovePosition(new Vector3(controller.position.x + xOff, controller.position.y + yOff, controller.position.z + zOff));
        
        //transform.position = controller.position + new Vector3 (xOff, yOff, zOff);
    }
}
