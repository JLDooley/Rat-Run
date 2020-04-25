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
 
    public GameObject fist;
    
    public Interactable interactable;

    public bool primed = true;

    public Animation punchAnimation;
        
        
    



        
    void Start()    
    {      
        interactable = GetComponent<Interactable>();
    }

        
    void Update()
    { 
        

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
            Debug.Log("Punching");
            primed = false;
            punchAnimation.Play();
        }
    }

    void Reprime()
    {
        Debug.Log("Repriming");
        primed = true;
    }
}
