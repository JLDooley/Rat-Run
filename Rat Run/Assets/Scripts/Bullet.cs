using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1.0f;
    //private Transform aimTarget;
    private Vector3 direction;

    void Start()
    {
        direction = Vector3.forward;

        Destroy(gameObject, 5.0f);
    }

    
    void Update()
    {
        transform.position += speed * Time.deltaTime * direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit"); 
        }
        Destroy(gameObject);

    }
}
