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
        //Ignore specific collisions (eg for shooting through a shield). Can't use layermasks because I want to collide with the 'hitbox' layer on the player
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(DelayedDestroy());
        }
        else
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider);
        }
        
    }

    IEnumerator DelayedDestroy()
    {
        //Small delay to ensure any physics impacts take place
        //Debug.Log("Destroying");
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
        //Debug.Log("Destroyed");
    }
}
