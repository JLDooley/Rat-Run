using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windscreen_Brackeys : MonoBehaviour
{
    public Transform playerCamera;
    public Transform windscreen;
    public Transform viewSpace;



    
    void Update()
    {
        Matrix4x4 viewSpaceMatrix = viewSpace.transform.localToWorldMatrix;
        Matrix4x4 windscreenMatrix = windscreen.transform.worldToLocalMatrix;
        Matrix4x4 playerCameraMatrix = playerCamera.transform.localToWorldMatrix;

        //var m = viewSpace.transform.localToWorldMatrix * windscreen.transform.worldToLocalMatrix * playerCamera.transform.localToWorldMatrix;

        var m = viewSpaceMatrix * windscreenMatrix * playerCameraMatrix ;

        transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        //Vector3 playerOffsetFromWindscreen = playerCamera.position - windscreen.position;
        //transform.position = viewSpace.position + playerOffsetFromWindscreen;
    }
}
