using RatRun.GameEngine.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
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


    public Transform refBaseTransform;
    public Transform refTargetTransform;


     
    public SixDOFMapping mapping;

    [Range(0f, 90f)]
    public float maxAngle;
    private float limitRadius;
    private float limitHeight;
    private float currentRadius;
    private Vector3 normal = new Vector3(0f, 1f, 0f);

    protected Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.DetachFromOtherHand;

    protected float initialMappingOffsetX;
    protected float initialMappingOffsetY;
    protected float initialMappingOffsetZ;

    protected float initialRotationOffsetX;
    protected float initialRotationOffsetY;
    protected float initialRotationOffsetZ;

    Quaternion offsetAngle;
    float offsetAngleX;
    float offsetAngleY;
    float offsetAngleZ;

    protected Interactable interactable;

    private Vector3 projection;
    private Vector3 newProjection;
    private Vector3 target;

    private Vector3 axis;
    float angle = 0f;

    public float gizmoSize = 0.01f;

    protected void Awake()
    {
        interactable = GetComponent<Interactable>();

        CalculateLimits();
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
        refBaseTransform.rotation.ToAngleAxis(out angle, out axis);
    }

    protected virtual void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            initialMappingOffsetX = mapping.xValue - CalculateLinearMapping(hand.transform, MappingValue.xValue);
            initialMappingOffsetY = mapping.yValue - CalculateLinearMapping(hand.transform, MappingValue.yValue);
            initialMappingOffsetZ = mapping.zValue - CalculateLinearMapping(hand.transform, MappingValue.zValue);

            /*offsetAngleX = hand.transform.localEulerAngles.x;
            //offsetAngleY = hand.transform.localEulerAngles.y;
            offsetAngleZ = hand.transform.localEulerAngles.z;*/

            offsetAngle = hand.transform.rotation;

            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
        }
    }

    protected virtual void HandAttachedUpdate(Hand hand)
    {
        UpdateLinearMapping(hand.transform);
        //Debug.Log(offsetAngle);

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

        //Rotate the rotation reference gameobjects by the controller's rotation minus the controller's initial offset
        refBaseTransform.transform.rotation = updateTransform.rotation * Quaternion.Inverse(offsetAngle);


        //Project the rotation reference target onto the x-z plane
        projection = Vector3.ProjectOnPlane(refTargetTransform.position - refBaseTransform.position, normal);


        //Calculate the current distance of the projected point from the origin, to see if it exceeds the limit defined by the limit radius
        float currentRadius = projection.magnitude;

        if (currentRadius < limitRadius)
        {
            baseTransform.rotation = refBaseTransform.rotation;

        }
        else
        {
            newProjection = projection.normalized;

            target = Create2DAxis(newProjection);
            Debug.Log("Target: " + target);

            baseTransform.rotation = Quaternion.AngleAxis(maxAngle, target);
        }

    }

    protected void CalculateLimits()
    {
        float distance = (refTargetTransform.position - refBaseTransform.position).magnitude;

        //Calculate the radius of a circle (on the xz-plane) representing the movement range of the joystick for a given angular limit
        maxAngle = 90 - maxAngle;
        limitRadius = distance * Mathf.Cos(Mathf.Deg2Rad * maxAngle);
        limitHeight = distance * Mathf.Sin(Mathf.Deg2Rad * maxAngle);
        Debug.Log("Limit Radius: " + limitRadius);
        Debug.Log("Limit Height: " + limitHeight);
    }

    protected void CalculateOrientation()
    {
        
    }

    protected Vector3 Create2DAxis(Vector3 direction)
    {
        if (direction.magnitude != 1)
        {
            direction = direction.normalized;
        }

        float ang;

        if (direction.x >= 0)
        {
            ang = Mathf.Asin(newProjection.z); // -90 <= ang <= 90
            if (ang < 0)
            {
                ang = ang + (Mathf.PI * 2);
            }
        }
        else
        {
            ang = Mathf.PI - Mathf.Asin(newProjection.z); // 90 <= ang <= 270
        }

        ang = (ang - (Mathf.PI / 2)) % (Mathf.PI * 2);

        return new Vector3(Mathf.Cos(ang), 0f, Mathf.Sin(ang));

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(refBaseTransform.position, refTargetTransform.position);
        Gizmos.DrawSphere(refTargetTransform.position, gizmoSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(refBaseTransform.position, projection + refBaseTransform.position);
        Gizmos.DrawLine(refTargetTransform.position, projection + refBaseTransform.position);
        Gizmos.DrawSphere(projection + refBaseTransform.position, gizmoSize);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(refBaseTransform.position, axis + refBaseTransform.position);
        Gizmos.DrawSphere(axis + refBaseTransform.position, gizmoSize);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(refBaseTransform.position, target + refBaseTransform.position);
        Gizmos.DrawSphere(target + refBaseTransform.position, gizmoSize);
    }
}
