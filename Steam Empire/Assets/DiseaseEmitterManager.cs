using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseEmitterManager : MonoBehaviour
{
    float previousCall = 0;
    float internalCooldown = 0.5f;
    public Canvas uiCanvas;
    UIController ui;
    // Start is called before the first frame update
    void Start()
    {
       ui  = uiCanvas.GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        float timeOfCall = Time.time;
        
        if (timeOfCall > previousCall + internalCooldown)
        {
            ui.setDiseasePercent(ui.getDiseasePercent() + 0.1f);
            previousCall = timeOfCall;
        }
    }
}
