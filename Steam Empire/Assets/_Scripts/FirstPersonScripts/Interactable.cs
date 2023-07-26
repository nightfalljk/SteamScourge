using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    /// <summary>
    /// Will display "Interact" if left empty
    /// </summary>
    public string interactMessage = string.Empty;

    [SerializeField] UnityEvent onInteract;

    public void Interact()
    {
        if(DialogueManager.GetInstance().dialogueIsPlaying) return;
        onInteract.Invoke();
    }
}
