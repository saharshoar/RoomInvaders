using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTest : MonoBehaviour
{
    public GameObject playerLight;
    public List<GameObject> planePointLights;
    public List<Material> emissionLights;
    private bool flashLightOn = true;
    public AudioSource intensity2Song;
    public AudioSource intensity3Song;

    private float lightsCounter = 0.5f;
    private float emissionsCounter = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        intensity2Song.volume = 0f;
        intensity3Song.volume = 0f;

        foreach (Material emission in emissionLights)
        {
            emission.DisableKeyword("_EMISSION");
        }
        foreach (GameObject pointLight in planePointLights)
        {
            pointLight.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FlashLightToggle();

        MapLightsToggle();
        MusicStage1Toggle();
        MusicStage2Toggle();
        
    }

    private void MapLightsToggle()
    {
        if (PowerSwitch.instance.powerOn)
        {
            foreach (Material emission in emissionLights)
            {
                lightsCounter -= Time.deltaTime;
                if (lightsCounter <= 0)
                {
                    emission.EnableKeyword("_EMISSION");
                    lightsCounter = 0.5f;
                }
            }
            foreach (GameObject pointLight in planePointLights)
            {
                emissionsCounter -= Time.deltaTime;
                if (emissionsCounter <= 0)
                {
                    pointLight.SetActive(true);
                    emissionsCounter = 0.5f;
                }
            }
        }
    }

    private void FlashLightToggle()
    {
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

    private void MusicStage1Toggle()
    {
        if (PowerSwitch.instance.powerOn)
        {
            if (intensity2Song.volume < 0.6f)
            {
                intensity2Song.volume += 0.1f * Time.deltaTime;
            }
        }
        else if (!PowerSwitch.instance.powerOn)
        {
            intensity2Song.volume = 0f;
        }
    }

    private void MusicStage2Toggle()
    {
        if (SpawnManager.instance.roundNumber >= 10)
        {
            if (intensity3Song.volume < 0.8f)
            {
                intensity3Song.volume += 0.1f * Time.deltaTime;
            }
        }
        else
        {
            intensity3Song.volume = 0f;
        }
    }
}
