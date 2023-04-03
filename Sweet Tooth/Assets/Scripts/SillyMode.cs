using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyMode : MonoBehaviour
{
    public Renderer playerRenderer;
    public Material material;

    void Start()
    {
        makeSilly(false);
    }

    public void makeSilly(bool isSilly)
    {
        if (isSilly)
        {
            // set material's rendering mode to both faces (hamper mode)
            material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        }
        else
        {
            // set material's rendering mode to both faces (hamper mode)
            material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
        }
        // assign modified material to player
        playerRenderer.material = material;
    }
}
