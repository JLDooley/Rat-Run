using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class SceneTransition : MonoBehaviour
{
    public int levelIndex;

    public Animator orbAnimator;
    public float animationRampTime = 0.5f;
    private float timer;

    protected Interactable interactable;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
        AnimateHandHover();

        timer += Time.deltaTime;
    }



    protected virtual void OnHandHoverBegin(Hand hand)
    {
        orbAnimator.SetBool("IsTouched", true);

        timer = 0f;
    }


    protected virtual void OnHandHoverEnd(Hand hand)
    {
        orbAnimator.SetBool("IsTouched", false);

        timer = 0f;
    }


    protected virtual void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType == GrabTypes.Grip)
        {
            Debug.Log("Transitioning");

            //Fade here

            TransitionToLevel(levelIndex);
        }
    }



    public void AnimateHandHover()
    {
        bool touchCheck = orbAnimator.GetBool("IsTouched");
        float currentAnimationSpeed = orbAnimator.GetFloat("SpinSpeedMultiplier");

        if (touchCheck && currentAnimationSpeed < 3f)
        {
            currentAnimationSpeed = RampOverTime(currentAnimationSpeed, 3f, animationRampTime, timer);
        }
        else if (!touchCheck && currentAnimationSpeed > 1f)
        {
            currentAnimationSpeed = RampOverTime(currentAnimationSpeed, 1f, animationRampTime, timer);
        }
        orbAnimator.SetFloat("SpinSpeedMultiplier", currentAnimationSpeed);
    }
    

    public void TransitionToLevel (int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }


    public float RampOverTime(float current, float target, float duration, float currenttime)
    {
        current = Mathf.Lerp(current, target, currenttime / duration);

        if (CalculationFunctions.FastApproximately(current, target, 0.01f))
        {
            current = target;
        }
        return current;
    }
}
