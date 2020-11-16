using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset_Calculator : MonoBehaviour
{

    [Header("Calculates the position and orientation of the gameObject relative to Target transform to match that of Reference Object relative to Reference Target")]
    [Tooltip("The Transform this gameObject is to be offset against.")]
    public Transform target;

    [Tooltip("The counterpart of this gameObject in a reference system.")]
    public Transform referenceObject;

    [Tooltip("The Transform the counterpart is offset against.")]
    public Transform referenceTarget;

    void Update()
    {
        Matrix4x4 m = target.transform.localToWorldMatrix * referenceTarget.transform.worldToLocalMatrix * referenceObject.transform.localToWorldMatrix;

        transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
    }
}
