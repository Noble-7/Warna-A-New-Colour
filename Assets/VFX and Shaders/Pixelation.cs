using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixelation : MonoBehaviour
{
    public Material m_pixelateMaterial;
    public int pixelDensity = 64;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        Vector2 aspectRatioData;
        if (Screen.height > Screen.width)
            aspectRatioData = new Vector2((float)Screen.width / Screen.height, 1);
        else
            aspectRatioData = new Vector2(1, (float)Screen.height / Screen.width);
        m_pixelateMaterial.SetVector("_AspectRatioMultiplier", aspectRatioData);
        m_pixelateMaterial.SetInt("_PixelDensity", pixelDensity);
        Graphics.Blit(source, destination, m_pixelateMaterial);
    }
}
