using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;
using UnityEngine.XR.Provider;
using Valve.VR;

public class WindscreenTextureSetup : MonoBehaviour
{
    public Camera viewportCamera;

    public Material viewportMaterial;
    void Start()
    {
        CreateRenderTexture();
        Debug.Log("VR Screen width: " + XRSettings.eyeTextureWidth);
        Debug.Log("VR Screen height: " + XRSettings.eyeTextureHeight);

        //Debug.Log("VR Screen width: ");
    }

    

    void Update()
    {
        if (Input.GetKeyDown("enter"))
        {
            CreateRenderTexture();
        }
    }

    private void CreateRenderTexture()
    {
        if (viewportCamera.targetTexture != null)
        {
            viewportCamera.targetTexture.Release();
        }

        Debug.Log("Screen width: " + Screen.width);

        Debug.Log("Screen height: " + Screen.height);


        //viewportCamera.targetTexture = new RenderTexture(XRSettings.eyeTextureWidth, XRSettings.eyeTextureHeight, 24);

        viewportCamera.targetTexture = new RenderTexture(1440, 1600, 24);

        //viewportCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        viewportMaterial.mainTexture = viewportCamera.targetTexture;
    }


}
