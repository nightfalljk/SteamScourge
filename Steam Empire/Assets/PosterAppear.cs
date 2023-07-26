using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PosterAppear : MonoBehaviour
{
    [SerializeField]
    private Image _Image;
    
    public int posterId;

    private Text _interactText;
    private Text _headline;
    private Text _description;
    private Text _signing;

    private string[] headlineList = 
        {
        "The foreman cares!",
        "Imperial Decree", 
        "See the physician!", 
        "Curfew!",
        "STEAM STONE",
        "Reduced rations"
        };
    private string[] descriptionList = 
        {
        "Come by the factory. \n Get your rations!",
        "All travel from and to this district only with an official pass.",
        "Notice strange colorations on your skin? \n Protect your fellow citizens! \n Go see the physician down by the canal near the burners!",
        "Everyone needs to stay inside after sundown. \n Disobedience will be punished!",
        "THE FOUNDATION OF OUR MIGHTY EMPIRE",
        "Due to supply shortages all rations will be reduced. No exceptions."
        };
    private string[] signingList =
        {
        "",
        "The Emperor's Office",
        "Your foreman",
        "Your foreman",
        "",
        "The foreman"
        };

    private void Awake()
    {
        Random random = new Random();
        int randomNumber = random.Next(0, headlineList.Length);
        


        _interactText = GameObject.Find("UIInteract").GetComponent<Text>();
        _headline = GameObject.Find("Headline").GetComponent<Text>();
        _description = GameObject.Find("Description").GetComponent<Text>();
        _signing = GameObject.Find("Signing").GetComponent<Text>();

        _interactText.enabled = false;
        _headline.enabled = false;
        _description.enabled = false;
        _signing.enabled = false;

        

        _Image.enabled = false;

    }
    private void Update()
    {
        
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
                _Image.enabled = true;
                _headline.enabled = true;
                _description.enabled = true;
                _signing.enabled = true;

                _interactText.enabled = false;

                _headline.text = headlineList[posterId];
                _description.text = descriptionList[posterId];
                _signing.text = signingList[posterId];

            }

        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            _Image.enabled = false;
            _headline.enabled = false;
            _description.enabled = false;
            _signing.enabled = false;
            _interactText.enabled = false;
        }
    }
}
