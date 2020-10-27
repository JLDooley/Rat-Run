using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSpeedCalculator : MonoBehaviour
{
    public Transform foot;

    public Vector3 maxPosition;
    public Vector3 minPosition;

    private float maxTime;
    private float minTime;

    public float speed;

    void Start()
    {
        maxPosition = foot.position;
        minPosition = maxPosition;
    }

    
    void Update()
    {
        if (foot.position.magnitude > maxPosition.magnitude)
        {
            maxPosition = foot.position;
            maxTime = Time.time;

        }
        else if (foot.position.magnitude < minPosition.magnitude)
        {
            minPosition = foot.position;
            minTime = Time.time;
        }

        float stride = (maxPosition - minPosition).magnitude;
        float dTime = maxTime - minTime;

        speed = stride / dTime;

        Debug.Log(stride);
    }
}
