using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System;

public class BoxingGlove : MonoBehaviour
{
        
    public SteamVR_Action_Boolean inputAction;

        
    public SteamVR_Input_Sources source;

        
    public Transform spawner;

        
    public GameObject fist;

        
    public Interactable interactable;

       
    private float timer = 0f;

    public bool primed = true;

        
    [Tooltip("Cooldown period (in seconds) between shots")]
        
    public float rateOfFire = 1f;



        
    void Start()    
    {      
        interactable = GetComponent<Interactable>();
    }

        
    void Update()
    { 
        timer -= Time.deltaTime;

        if (interactable.attachedToHand)
        {
            Debug.Log("Hand Detected");
            source = interactable.attachedToHand.handType;
 
            if (inputAction.GetState(source))
            {
                    Debug.Log("Punching");
                    Punch();
            }
        }    
    }

        
    void Punch()  
    {  
        Debug.Log("Punch() Run");
 
        if (primed)
        { 

        }
    }
}
