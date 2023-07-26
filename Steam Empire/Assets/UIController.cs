using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField] private InputActionAsset MouseInputAction; 
    public GameObject blackOutSquare;
    public AudioSource cutsceneAudio;

    private void Start()
    {
        cutsceneAudio = GetComponent<AudioSource>();
    }
    public void Update() {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.GetComponentInChildren<MenuManager>(true).gameObject.active == false)
            {
                MouseInputAction.actionMaps[1].Enable();

                gameObject.GetComponentInChildren<MenuManager>(true).gameObject.SetActive(true);
                Cursor.visible = true;
                //FindObjectOfType<PlayerControl>().disablePlayerControls();
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    public void cutsceneFadeToBlack()
    {
        StartCoroutine(FadeBlackOutSquare());
    }

    public void cutsceneFadeAwayFromBlack()
    {
        cutsceneAudio = GetComponent<AudioSource>();
        StartCoroutine(FadeBlackOutSquare(false));
    }

    public void cutscenePlayAudio()
    {
        // Currently being played on scene load instead
        //cutsceneAudio.Play();
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 2)
    {
        Color objectColor = blackOutSquare.GetComponent<RawImage>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            Debug.Log("fading to black");
            while (blackOutSquare.GetComponent<RawImage>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<RawImage>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            Debug.Log("fading away from black");
            while (blackOutSquare.GetComponent<RawImage>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<RawImage>().color = objectColor;
                yield return null;
            }
        }
    }

    public void setDiseasePercent(float newPercent)
    {
        gameObject.GetComponentInChildren<DiseaseBarManager>().setDiseasePercentage(newPercent);
    }

    public float getDiseasePercent()
    {
        return gameObject.GetComponentInChildren<DiseaseBarManager>().getDiseasePercentage();
    }

    
}
