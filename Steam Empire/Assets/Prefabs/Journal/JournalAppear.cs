using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class JournalAppear : MonoBehaviour
{
   // [SerializeField]
    private Canvas _journalCanvas;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("Setup");
        StreamWriter outStream = new StreamWriter(Application.dataPath + "/Resources/journal2.txt");
        outStream.WriteLine("1 clue:true:false");
        outStream.WriteLine("2 clue:false:false");
        outStream.WriteLine("3 clue:false:false");
        outStream.WriteLine("4 clue:false:false");
        outStream.WriteLine("5 clue:false:false");
        outStream.WriteLine("6 clue:false:false");
        outStream.WriteLine("7 clue:false:false");
        outStream.WriteLine("8 clue:false:false");
        outStream.Close();
    }


    private void Start()
    {
        _journalCanvas = GameObject.Find("DiaryCanvas").GetComponent<Canvas>();
        _journalCanvas.enabled = false;
    }

    private void Update()
    {
        
            if (Input.GetKeyDown("tab"))
            {
                _journalCanvas.enabled = !_journalCanvas.enabled;
                ReadString(_journalCanvas);
            }
            
    }
    
    static void ReadString(Canvas journalCanvas)
    {

        string path = Application.dataPath+ "/Resources/journal2.txt";
        //Read the text from directly from the test.txt file
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                String[] lines = line.Split(':');
                var tmpAsset = journalCanvas.transform.Find(lines[0]).GetComponent<TMP_Text>();
                print(tmpAsset);
                tmpAsset.enabled = bool.Parse(lines[1]);
                var strikethrough = bool.Parse(lines[2]);
                if (strikethrough)
                    tmpAsset.fontStyle = FontStyles.Strikethrough;
                else
                    tmpAsset.fontStyle = FontStyles.Normal;
            }
            reader.Close();
        }
        

    }
}
