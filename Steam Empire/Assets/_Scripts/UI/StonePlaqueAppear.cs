using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StonePlaqueAppear : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text _interactText;

    private void Awake()
    {
        _interactText.enabled = false;
        image.enabled = false;

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            _interactText.enabled = true;
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (Input.GetKey("e")) {
                image.enabled = true;
                _interactText.enabled = false;
            }
        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            image.enabled = false;
            _interactText.enabled = false;
        }
    }
}
