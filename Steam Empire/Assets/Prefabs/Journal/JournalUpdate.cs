using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using _Scripts.Audio;
using UnityEditor;
using UnityEngine;

public class JournalUpdate : MonoBehaviour
{
    // [SerializeField]
    private Canvas _journalCanvas;
    private GameObject _journalNotification;
    public int storyProgression = 1;
    public AudioClip notifyClip;

    private void Start()
    {
        _journalCanvas = GameObject.Find("DiaryCanvas").GetComponent<Canvas>();
        _journalNotification = GameObject.Find("JournalNotification");
        _journalNotification.SetActive(false);
        _journalCanvas.enabled = false;
    }


    public static void WriteString(Canvas journalCanvas, int entry, bool scribble)
    {


        int line_to_edit = entry; // Warning: 1-based indexing!
        string sourceFile = Application.dataPath + "/Resources/journal2.txt";
        string destinationFile = Application.dataPath + "/Resources/journal2.txt";

        // Read the appropriate line from the file.
        string lineToWrite = null;
        using (StreamReader reader = new StreamReader(sourceFile))
        {
            for (int i = 1; i <= line_to_edit; ++i)
                lineToWrite = reader.ReadLine();
            
        }

        if (lineToWrite == null)
            throw new InvalidDataException("Line does not exist in " + sourceFile);
        
        if (scribble)
            lineToWrite = entry.ToString() + " clue:true:true";
        else
            lineToWrite = entry.ToString() + " clue:true:false";

        // Read the old file.
        string[] lines = File.ReadAllLines(destinationFile);

        

        // Write the new file over the old file.
        using (StreamWriter writer = new StreamWriter(destinationFile))
        {
            for (int currentLine = 1; currentLine <= lines.Length; ++currentLine)
            {
                if (currentLine == line_to_edit)
                {
                    writer.WriteLine(lineToWrite);
                }
                else
                {
                    writer.WriteLine(lines[currentLine - 1]);
                }
            }
        }



    }

    public void restoreToDefaults()
    {
        string path = Application.dataPath + "/Resources/journal2.txt";
        File.WriteAllText(path, String.Empty);
        TextWriter tw = new StreamWriter(path, true);
        tw.WriteLine("1 clue:true:false");

        int amountOfEntries = 8;
        
        for (int i = 2; i <= amountOfEntries; i++)
            tw.WriteLine(i.ToString() + " clue:false:false");
        
        tw.Close();
        StartCoroutine(NotifyPlayer());
    }

    public void updateJournal(int entry, bool scribble)
    {
        WriteString(_journalCanvas, entry, scribble);

        if (!scribble)
        {
            storyProgression = entry;

            StartCoroutine(NotifyPlayer());
        }

    }

    private IEnumerator NotifyPlayer()
    {
        AudioUtility.CreateSFX(notifyClip, transform, 0, PlayerPrefs.GetFloat("GlobalVolume"));
        var active = false;
        for (int i = 0; i < 8; i++)
        {
            active = !active;
            _journalNotification.SetActive(active);
            yield return new WaitForSeconds(1f);
        }
    }
}
