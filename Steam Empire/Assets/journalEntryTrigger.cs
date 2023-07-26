using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class journalEntryTrigger : MonoBehaviour
{
    public int entryToBeWritten = -1;
    public int entryToBeScribbled= -1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            JournalUpdate journal = GameObject.FindGameObjectWithTag("Player").GetComponent<JournalUpdate>();
            if (journal.storyProgression == entryToBeWritten - 1) //to be revisited if story isnt lineal
            {
                if (entryToBeWritten != -1)
                    journal.updateJournal(entryToBeWritten, false);

                if (entryToBeScribbled != -1)
                    journal.updateJournal(entryToBeScribbled, true);
                
                gameObject.SetActive(false);
            }
        }
    }
}
