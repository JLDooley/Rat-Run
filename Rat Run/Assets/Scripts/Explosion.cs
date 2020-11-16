using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public void Explode(float explosionRadius, LayerMask explosionLayer)
    {
        Debug.Log("Exploding");
        Vector3 centre = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(centre, explosionRadius, explosionLayer);

        Debug.Log("Collider Array Size: " + hitColliders.Length);

        foreach (var hitCollider in hitColliders)
        {
            GameObject hitObject = hitCollider.gameObject;
            
            if (!hitObject.CompareTag("Player"))
            {
                Debug.Log(hitObject.name);
                hitObject.GetComponent<Target>().Die();
            }
        }

        Destroy(gameObject, 4f);
    }
}
