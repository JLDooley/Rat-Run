using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 30f;

    public GameObject parentEntity;

    private void Start()
    {
        if (parentEntity == null)
        {
            parentEntity = gameObject;
            Debug.Log(gameObject.name + ": No parent assigned, assigning this gameObject");
        }
    }

    public void TakeDamage (float amount)
    {
        Debug.Log("Damage Recieved");
        
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(parentEntity);
    }
}
