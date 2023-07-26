using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadingInCutscene : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject cutsceneTeleportTarget;
    void Start()
    {
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            cutsceneEnd();
        }
        
    }

    public void cutsceneFadeToBlack()
    {
        StartCoroutine(uiCanvas.GetComponent<UIController>().FadeBlackOutSquare());
    }

    public void cutsceneFadeAwayFromBlack()
    {
        StartCoroutine(uiCanvas.GetComponent<UIController>().FadeBlackOutSquare(false));
    }

    public void cutscenePlaySound()
    {
        //uiCanvas.GetComponent<UIController>().cutscenePlayAudio();
    }

    public void cutsceneEnd()
    {
        gameObject.SetActive(false);
        FindObjectOfType<PlayerControl>().teleportPlayer(cutsceneTeleportTarget);
        FindObjectOfType<PlayerControl>().enablePlayerControls();
        
    }
}
