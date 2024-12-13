using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField] Material firstMaterial;
    [SerializeField] Material sunsetSkyMaterial;
    private bool buttonTrigger = false;

    void Start()
    {
        RenderSettings.skybox = firstMaterial;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (!buttonTrigger)
            {
                if (RenderSettings.skybox == firstMaterial)
                {
                    RenderSettings.skybox = sunsetSkyMaterial;
                }
                else if (RenderSettings.skybox == sunsetSkyMaterial)
                {
                    RenderSettings.skybox = firstMaterial;
                }
                buttonTrigger = true;
            }
        }
        else
            buttonTrigger = false;
    }
}