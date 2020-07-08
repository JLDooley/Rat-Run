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
        direction = transform.forward;
        
        

        Destroy(gameObject, 5.0f);
    }

    
    void Update()
    {
        transform.position += speed * Time.deltaTime * direction.normalized;
    }


    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        Debug.Log("Destroying");
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
        Debug.Log("Destroyed");
    }
}
