using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Windscreen : MonoBehaviour
{
    public GameObject linkedWindscreen;
    //public MeshRenderer screen;
    public Camera playerCam;
    Camera portalCam;
    RenderTexture viewTexture;

    void Awake()
    {
        //playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        portalCam.enabled = false;
    }

    void Update()
    {
        CreateViewTexture();
    }

    void CreateViewTexture()
    {
        if (viewTexture != null)
        {
            viewTexture.Release();
        }
        viewTexture = new RenderTexture(Screen.width, Screen.height, 0);

        portalCam.targetTexture = viewTexture;

        //Display on the screen of the other portal (the windscreen)
        linkedWindscreen.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", viewTexture);
    }

    //public void Render()
    //{
    //    screen.enabled = false;
    //    CreateViewTexture();

    //    var m = transform.localToWorldMatrix * linkedWindscreen.transform.worldToLocalMatrix * playerCam.transform.localToWorldMatrix;
    //    portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

    //    portalCam.Render();

    //    screen.enabled = true;
    //}
}
