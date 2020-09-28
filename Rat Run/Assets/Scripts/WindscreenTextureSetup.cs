using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindscreenTextureSetup : MonoBehaviour
{
    public Camera viewportCamera;

    public Material viewportMaterial;
    void Start()
    {
        if (viewportCamera.targetTexture != null)
        {
            viewportCamera.targetTexture.Release();
        }

        viewportCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        viewportMaterial.mainTexture = viewportCamera.targetTexture;

    }


}
