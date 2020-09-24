using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public Transform drivenCamera;
    public float speed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("w"))
        //{
            drivenCamera.Translate(Vector3.forward * speed * Time.deltaTime);
        //}
    }
}
