using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]


// Controls movement and orientation of the attached gameObject, should be attached to the root of the hierarchy to be moved
public class Pathing : MonoBehaviour
{
    public Transform[] path;
    public bool orientToPath = false;

    public float velocity = 1f;
    public float targetVelocity = 1f;
    public float acceleration = 1f;

    private float pathLength;
    private float pathPosition = 0f;

    private Vector3 previousPosition = Vector3.zero;
    private Vector3 currentPosition = Vector3.zero;

    public float currentVelocity;

    public float lookAheadAmount = 0.01f;

    private Vector3 lookTarget;
    private Vector3 aimVec;
    private Quaternion aimQuat;

    public float rotationSpeed;

    public EnemyAI characterAI;


    void Start()
    {
        if (path.Length >= 2)
        {
            pathLength = iTween.PathLength(path);
        }


        if (characterAI == null)
        {
            characterAI = GetComponent<EnemyAI>();
        }
    }

    private void Update()
    {
        if (path.Length >= 2)
        {
            PathPosition();
        }

        if (path.Length > 0)
        {
            CalculateVelocity();
            MoveCharacter();
        }



        CalculateOrientation();

        if (pathPosition >= 1 && velocity != 0)
        {
            StopCharacter();
        }
    }


    void StopCharacter()
    {
        targetVelocity = 0f;
        Debug.Log(characterAI.name + ": Stopping, end of path reached.");

        if (velocity < 0)
        {
            velocity = 0;   //Prevent a bug where setting target velocity to 0 would make the gameobject reverse direction and accelerate)
        }
    }

    private void PathPosition()
    {
        float changeRate = velocity * Time.deltaTime / pathLength;

        //pathPosition += changeRate;
        pathPosition = Mathf.Max(pathPosition + changeRate, 0f);
    }

    private void MoveCharacter()
    {
        if (path.Length >= 2)
        {
            Vector3 coordinateOnPath = iTween.PointOnPath(path, pathPosition);

            gameObject.transform.position = coordinateOnPath;
        }
        else if (path.Length == 1)
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", path[0], "speed", targetVelocity));
        }


    }

    private void CalculateVelocity()
    {
        previousPosition = currentPosition;
        currentPosition = transform.position;

        currentVelocity = ((currentPosition - previousPosition).magnitude) / Time.deltaTime;

        //  If using the PointOnPath method, velocity can change depending on the distance between nodes
        if (path.Length >= 2)
        {
            //  Adjust the speed to compensate, unless too close to merit a change
            if (!CalculationFunctions.FastApproximately(currentVelocity, targetVelocity, 0.01f))
            {
                //Debug.Log("Calculating");

                if (currentVelocity > targetVelocity)       // Too fast, slow down
                {
                    velocity -= (acceleration * Time.deltaTime);
                }
                else if (currentVelocity < targetVelocity)  // Too slow, speed up
                {
                    velocity += (acceleration * Time.deltaTime);
                }
            }
        }

    }

    private void CalculateOrientation()
    {
        if (characterAI.trackPlayer)
        {
            lookTarget = characterAI.player.playerPosition.position;

            //Debug.Log("Tracking Player at: " + lookTarget);
        }
        else
        {
            if (path.Length >= 2)
            {
                lookTarget = iTween.PointOnPath(path, pathPosition + lookAheadAmount);
            }
            else if (path.Length == 1)
            {
                lookTarget = path[0].position;
            }
            else
            {
                lookTarget = transform.position + transform.forward;
                //Debug.Log(lookTarget);
            }

        }

        //Project direction to target onto plane, and rotate towards it (use RotateTowards() so that it can update/chase the target)
        aimVec = Vector3.ProjectOnPlane(lookTarget-transform.position, Vector3.up);
        aimQuat = Quaternion.LookRotation(aimVec);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, aimQuat, rotationSpeed*Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        iTween.DrawPath(path);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, lookTarget);
    }


}
