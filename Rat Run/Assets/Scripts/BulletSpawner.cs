using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR;
using Valve.VR.InteractionSystem;
using Valve.VR;



public class BulletSpawner : MonoBehaviour
{
    public SteamVR_Action_Single inputAction;

    public Hand hand;

    public GameObject spawner;

    public GameObject prefab;

    public Interactable interactable;

    private float timer = 0f;
    
    [Tooltip("Cooldown period (in seconds) between shots")]
    public float rateOfFire = 1f;

    void Start()
    {
        interactable = GetComponent<Interactable>();
        
    }

    void Update()
    {
        if (interactable.attachedToHand)
        {
            Debug.Log("Hand Detected");
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if (inputAction[source].active)
            {
                Shoot();
            }
        }

        
    }

    void Shoot()
    {
        Debug.Log("Shoot() Run");
        if (timer <= 0f)
        {
            Instantiate(prefab, spawner.transform);
            timer = rateOfFire;
        }
        
    }
}
