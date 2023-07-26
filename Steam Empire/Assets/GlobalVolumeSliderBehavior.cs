using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalVolumeSliderBehavior : MonoBehaviour
{
    public Slider globalVolumeSlider;
    private float globalVolume;
    void Start()
    {
        globalVolumeSlider.minValue = 0f;
        globalVolumeSlider.maxValue = 1f;
       
        if (PlayerPrefs.GetFloat("GlobalVolume", -1) != -1)
        {
            globalVolumeSlider.value = PlayerPrefs.GetFloat("GlobalVolume");
        }
        else
        {
            globalVolumeSlider.value = 0.5f;
            PlayerPrefs.SetFloat("GlobalVolume", 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setGlobalVolume(float value)
    {
        PlayerPrefs.SetFloat("GlobalVolume", value);
    }
}
