using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushEvent : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueAsset;

    [SerializeField] private Canvas uiCanvas;
    
    [SerializeField] private GameObject [] obstacles;

    [SerializeField] private Collider collider;

    public GameObject endOfAmbushTeleport;
    

    //TODO: Maybe set position and cam rotation for player character to face main hoodlum properly
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerControl>();
        if (player != null)
        {
            dialogueManager.AssignStory(dialogueAsset);
            dialogueManager.dialogueExit.AddListener(EndScene);
            dialogueManager.InitDialogue();
            collider.enabled = false;
        }
    }

    //TODO: Add input lock

    private void EndScene()
    {
        //Need to find a better way to do this, also issue with story when object disabled
        StartCoroutine(Knockout());
        StartCoroutine(uiCanvas.GetComponent<UIController>().FadeBlackOutSquare(fadeSpeed:4));
    }

    private IEnumerator Knockout()
    {
        //Replace coroutine with something event-driven, maybe reactive? Needs to play as soon as dialogue is over
        //and players might skip
        yield return new WaitForSeconds(4f);
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }
        dialogueManager.dialogueExit.RemoveAllListeners();

        StartCoroutine(uiCanvas.GetComponent<UIController>().FadeBlackOutSquare(fadeToBlack:false));
        FindObjectOfType<PlayerControl>().teleportPlayer(endOfAmbushTeleport);

        //UGLY BUT WORKS
        /*JournalUpdate journal = GameObject.FindGameObjectWithTag("Player").GetComponent<JournalUpdate>();
        journal.updateJournal(3, false);
        journal.updateJournal(2, true);*/
    }
    
}
