using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightHandler : MonoBehaviour
{
    void Update()
    {
        if (!GameManager.instance.isDay)
        {
            foreach (var light in FindObjectsOfType<Light2D>())
            {
                if (light.CompareTag("Light"))
                {
                    light.enabled = true;
                }
            }
        }
        else
        {
            foreach (var light in FindObjectsOfType<Light2D>())
            {
                if (light.CompareTag("Light"))
                {
                    light.enabled = false;
                }
            }
        }
    }
}
