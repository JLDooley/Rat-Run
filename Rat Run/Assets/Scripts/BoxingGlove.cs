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

    public Punch targetFist;

    public Interactable interactable;








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
                targetFist.Punching();
            }
        }
    }
}