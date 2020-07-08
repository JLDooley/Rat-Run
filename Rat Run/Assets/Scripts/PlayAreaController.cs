using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaController : MonoBehaviour
{
    [Tooltip("Desired head position of player when seated")]
    public Transform desiredHeadPosition;

    //Assign these variables in the inspector, or find them some other way (eg. in Start() )
    public Transform steamCamera;
    public Transform cameraRig;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (desiredHeadPosition != null)
            {
                ResetSeatedPos(desiredHeadPosition);
            }
            else
            {
                Debug.LogError("Target Transform required. Assign in inspector.", gameObject);
            }
        }
    }

    private void ResetSeatedPos(Transform desiredHeadPos)
    {
        if ((steamCamera != null) && (cameraRig != null))
        {
            //ROTATION
            // Get current head heading in scene (y-only, to avoid tilting the floor)
            float offsetAngle = steamCamera.rotation.eulerAngles.y - desiredHeadPos.rotation.eulerAngles.y;
            // Now rotate CameraRig in opposite direction to compensate
            cameraRig.Rotate(0f, -offsetAngle, 0f);

            //POSITION
            // Calculate postional offset between CameraRig and Camera
            
            //Vector3 offsetPos = steamCamera.position - cameraRig.position;
            //offsetPos = new Vector3(offsetPos.x, 0f, offsetPos.z);

            Vector3 offsetPos = Vector3.ProjectOnPlane(steamCamera.position - cameraRig.position, Vector3.up);

            // Reposition CameraRig to desired position minus offset

            Vector3 targetPosition = new Vector3(desiredHeadPos.position.x, 0f, desiredHeadPos.position.z);

            cameraRig.position = (targetPosition - offsetPos);

            Debug.Log("Seat recentered!");
        }
        else
        {
            Debug.Log("Error: SteamVR objects not found!");
        }
    }
}
