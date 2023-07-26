using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public Canvas uiCanvas;
    public GameObject cutsceneTeleportTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(uiCanvas.GetComponent<UIController>().FadeBlackOutSquare(fadeToBlack: false));
            FindObjectOfType<PlayerControl>().teleportPlayer(cutsceneTeleportTarget);
            FindObjectOfType<PlayerControl>().enablePlayerControls();
            gameObject.SetActive(false);
        }

    }

    public void endCutscene()
    {
        StartCoroutine(uiCanvas.GetComponent<UIController>().FadeBlackOutSquare(fadeToBlack: false));
        FindObjectOfType<PlayerControl>().teleportPlayer(cutsceneTeleportTarget);
        FindObjectOfType<PlayerControl>().enablePlayerControls();
        gameObject.SetActive(false);
    }
}
