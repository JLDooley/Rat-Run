using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour
{
    public Interactable interactable;
    public SteamVR_Action_Boolean inputAction;
    public SteamVR_Input_Sources source;

    public bool isHitScan = true;
    public float damage = 10f;
    public float range = 100f;

    public Transform rayOrigin;

    public ParticleSystem muzzleFlash;

    public GameObject tracerEffect;
    public GameObject impactEffect;

    void Start()
    {
        if (interactable == null)
        {
            interactable = GetComponent<Interactable>();
        }

        if (interactable == null)
        {
            Debug.LogError(gameObject.name + ": No interactable detected.");
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.attachedToHand)
        {
            source = interactable.attachedToHand.handType;

            if (inputAction[source].stateDown)
            {
                Debug.Log("Shooting");
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (isHitScan)
        {
            muzzleFlash.Play();
            
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, range))
            {
                Target target = hit.transform.GetComponent<Target>();

                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                GameObject tracerGO = Instantiate(tracerEffect);
                tracerGO.GetComponent<LineRenderer>().SetPosition(0, rayOrigin.position);
                tracerGO.GetComponent<LineRenderer>().SetPosition(1, hit.point);
                Destroy(tracerGO, 0.4f);

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }

        }
        
    }
}
