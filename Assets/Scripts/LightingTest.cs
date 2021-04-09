using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTest : MonoBehaviour
{
    public GameObject playerLight;
    public List<GameObject> planePointLights;
    public List<Material> emissionLights;
    private bool powerOn = false;
    private bool flashLightOn = true;
    public AudioSource intensity2Song;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientIntensity = 0f;
        RenderSettings.reflectionIntensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!powerOn)
            {
                //RenderSettings.ambientIntensity = 1f;
               // RenderSettings.reflectionIntensity = 1f;
               foreach (Material emission in emissionLights)
                {
                    emission.EnableKeyword("_EMISSION");
                }
                foreach (GameObject pointLight in planePointLights)
                {
                    pointLight.SetActive(true);
                }

                intensity2Song.volume = 0.45f;

                powerOn = true;
            }
            else if (powerOn)
            {
                // RenderSettings.ambientIntensity = 0f;
                // RenderSettings.reflectionIntensity = 0f;
                foreach (Material emission in emissionLights)
                {
                    emission.DisableKeyword("_EMISSION");
                }
                playerLight.SetActive(true);
                foreach (GameObject pointLight in planePointLights)
                {
                    pointLight.SetActive(false);
                }

                intensity2Song.volume = 0f;

                powerOn = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashLightOn)
            {
                playerLight.SetActive(false);
                flashLightOn = false;
            }
            else if (!flashLightOn)
            {
                playerLight.SetActive(true);
                flashLightOn = true;
            }
        }
    }
}
