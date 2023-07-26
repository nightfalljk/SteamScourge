using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BrightnessSliderBehavior : MonoBehaviour
{
    public Slider brightnessSlider;
    private Volume globalVolume;
    LiftGammaGain liftGammaGain;
    void Start()
    {
        brightnessSlider.minValue = 0.6f;
        brightnessSlider.maxValue = 1.6f;
        Debug.Log("initiated with gamma" + PlayerPrefs.GetFloat("Gamma", -1));
        if (PlayerPrefs.GetFloat("Gamma", -1) != -1)
        {
            brightnessSlider.value = PlayerPrefs.GetFloat("Gamma");
            Debug.Log("initiated with gamma" + brightnessSlider.value);
        }
        else
        {
            brightnessSlider.value = 1.0f;
            PlayerPrefs.SetFloat("Gamma", 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setBrightness(float value)
    {
        if (globalVolume == null)
            globalVolume = GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<Volume>();
        
        if (!globalVolume.profile.TryGet(out liftGammaGain)) throw new System.NullReferenceException(nameof(liftGammaGain));
        
        liftGammaGain.gamma.Override(new Vector4(1f, 1f, 1f, value-1));
    }
}
