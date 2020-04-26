using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Punch : MonoBehaviour
{
    public bool primed = true;

    public Animation punchAnimation;

    public void Punching()
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

