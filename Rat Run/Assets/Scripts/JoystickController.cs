using RatRun.GameEngine.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class JoystickController : MonoBehaviour
{
    public enum MappingValue
    {
        xValue,
        yValue,
        zValue
    };
    
    //Extents of the joystick's movement
    public Transform startTransform;
    public Transform endTransform;
    
    //Position and orientation trackers of the joystick
    public Transform baseTransform;
    public Transform refTransform;
     
    public SixDOFMapping mapping;

    protected Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.DetachFromOtherHand;

    protected float initialMappingOffsetX;
    protected float initialMappingOffsetY;
    protected float initialMappingOffsetZ;

    protected float initialRotationOffsetX;
    protected float initialRotationOffsetY;
    protected float initialRotationOffsetZ;

    Quaternion offsetAngle;

    protected Interactable interactable;

    protected void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        initialMappingOffsetX = mapping.xValue;
        initialMappingOffsetY = mapping.yValue;
        initialMappingOffsetZ = mapping.zValue;

        initialRotationOffsetX = mapping.xAngle;
        initialRotationOffsetY = mapping.yAngle;
        initialRotationOffsetZ = mapping.zAngle;

        

        UpdateLinearMapping(transform);
    }

    
    void Update()
    {
        UpdatePosition();
    }

    protected virtual void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            initialMappingOffsetX = mapping.xValue - CalculateLinearMapping(hand.transform, MappingValue.xValue);
            initialMappingOffsetY = mapping.yValue - CalculateLinearMapping(hand.transform, MappingValue.yValue);
            initialMappingOffsetZ = mapping.zValue - CalculateLinearMapping(hand.transform, MappingValue.zValue);

            offsetAngle = hand.transform.rotation;

            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
        }
    }

    protected virtual void HandAttachedUpdate(Hand hand)
    {
        UpdateLinearMapping(hand.transform);

        if (hand.IsGrabEnding(this.gameObject))
        {
            hand.DetachObject(gameObject);
        }
    }

    protected void UpdateLinearMapping(Transform updateTransform)
    {
        mapping.xValue = Mathf.Clamp01(initialMappingOffsetX + CalculateLinearMapping(updateTransform, MappingValue.xValue));
        mapping.yValue = Mathf.Clamp01(initialMappingOffsetY + CalculateLinearMapping(updateTransform, MappingValue.yValue));
        mapping.zValue = Mathf.Clamp01(initialMappingOffsetZ + CalculateLinearMapping(updateTransform, MappingValue.zValue));

        UpdatePosition();

        UpdateRotation(updateTransform);
    }

    protected float CalculateLinearMapping(Transform updateTransform, MappingValue targetValue = MappingValue.xValue)
    {
        Vector3 direction = endTransform.position - startTransform.position;
        Vector3 displacement = updateTransform.position - startTransform.position;

        return displacement[(int)targetValue] / direction[(int)targetValue];

    }

    protected void UpdatePosition()
    {
        float xComponent = Mathf.Lerp(startTransform.position.x, endTransform.position.x, mapping.xValue);
        float yComponent = Mathf.Lerp(startTransform.position.y, endTransform.position.y, mapping.yValue);
        float zComponent = Mathf.Lerp(startTransform.position.z, endTransform.position.z, mapping.zValue);
        transform.position = new Vector3(xComponent, yComponent, zComponent);
    }

    protected void UpdateRotation(Transform updateTransform)
    {
        transform.rotation = Quaternion.Euler(updateTransform.rotation.eulerAngles - offsetAngle.eulerAngles);
    }

}
