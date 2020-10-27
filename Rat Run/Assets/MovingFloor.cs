using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    public float startPoint;
    public float endPoint;

    public float velocity = 1f;

    private float y;
    private float z;

    private void Awake()
    {
        y = transform.position.y;
        z = transform.position.z;
    }

    void Update()
    {
        transform.position -= new Vector3(velocity * Time.deltaTime, 0f, 0f);

        if (transform.position.x <= endPoint)
        {
            transform.position = new Vector3(startPoint,y,z);
        }
    }
}
