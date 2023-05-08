using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyMode : MonoBehaviour
{
    public Renderer playerRenderer;
    public Material material;
    public bool sillyMode;

    void Start()
    {
        makeSilly(false);
    }

    void Update()
    {
        if (sillyMode)
        {
            // flash random colors for the sky and fog
            RenderSettings.skybox.SetColor("_SkyTint", new Color(Random.value*2f, Random.value*2f, Random.value*2f));
            RenderSettings.fogColor = new Color(Random.value*2f, Random.value*2f, Random.value*2f);
        }
    }

    public void makeSilly(bool isSilly)
    {
        sillyMode = isSilly;
        if (isSilly)
        {
            // set material's rendering mode to both faces (hamper mode)
            material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        }
        else
        {
            // set material's rendering mode to both faces (hamper mode)
            material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
            // I don't know why "Back" sets it to Front but whatever
        }
        // assign modified material to player
        playerRenderer.material = material;
    }
}
