using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiseaseBarManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDiseasePercentage(float newPercent)
    {
        if (newPercent >= 0 && newPercent <= 1.0)
            gameObject.GetComponent<Image>().fillAmount = newPercent;
    }

    public float getDiseasePercentage()
    {
        return gameObject.GetComponent<Image>().fillAmount;
    }
}
