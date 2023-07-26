using System.Collections;
using System.Collections.Generic;
using _Scripts.Audio;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    //[SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string AUDIO_TAG = "audio";

    private DialogueVariables dialogueVariables;
    private bool dialogueSoundIsPlaying;

    private UnityEvent _dialogueExit;
    
    public Story GetCurrentStory => currentStory;

    public UnityEvent dialogueExit => _dialogueExit;

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        _dialogueExit = new UnityEvent();
        dialogueVariables.ClearVariables(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    private void Start() 
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) 
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update() 
    {
        // clear storyline
        if (Input.GetKeyDown(KeyCode.F4))
        {
            dialogueVariables.ClearVariables(loadGlobalsJSON);
            JournalUpdate journal = GameObject.FindGameObjectWithTag("Player").GetComponent<JournalUpdate>();
            journal.restoreToDefaults();
        }
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying) 
        {
            return;
        }
        // handle continuing to the next line in the dialogue when key is pressed
        if (canContinueToNextLine 
            && currentStory.currentChoices.Count == 0 
            && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)))
        {
            ContinueStory();
        }
        if (Input.GetKeyDown(KeyCode.Q)) StartCoroutine(ExitDialogueMode());
    }

    public void EnterDialogueMode(TextAsset inkJSON) 
    {
        AssignStory(inkJSON);
        InitDialogue();
    }

    public void AssignStory(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
    }

    public void InitDialogue()
    {
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        // reset portrait speaker
        displayNameText.text = "unknown";

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode() 
    {
        JournalUpdate journal = GameObject.FindGameObjectWithTag("Player").GetComponent<JournalUpdate>();
        var entryToBeWritten = -1;
        var entryToBeScribbled = -1;
        
        if ((bool)currentStory.variablesState["ambushed"] == true)
        {
            entryToBeScribbled = 2;
            entryToBeWritten = 3;
        }
        
        if ((bool)currentStory.variablesState["talked_to_foreman"] == true)
        {
            entryToBeScribbled = 6;
            entryToBeWritten = 7;
        }
        
        if ((bool)currentStory.variablesState["doctor_approached"] == true)
        {
            entryToBeScribbled = 7;
            entryToBeWritten = 8;
        }
        
        journal.updateJournal(entryToBeScribbled, true);
        journal.updateJournal(entryToBeWritten, false);
        
        _dialogueExit.Invoke();
        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        _dialogueExit.RemoveAllListeners();
    }

    private void ContinueStory(bool handleTags = true) 
    {
        if (currentStory.canContinue) 
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null) 
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            // handle tags
            if(handleTags) HandleTags(currentStory.currentTags);
        }
        else 
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        bool fasterTyping = false;
        // set the text to the full line, but set the visible characters to 0
        if (line.StartsWith("#") || line.Trim().Length == 0)
        {
            ContinueStory(false);
            yield break;
        }
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
       // continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            // if (InputManager.GetInstance().GetSubmitPressed()) 
            // {
            //     dialogueText.maxVisibleCharacters = line.Length;
            //     break;
            // }
            fasterTyping = Input.GetKey(KeyCode.Space);

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag) 
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else 
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(fasterTyping ? 0.01f : typingSpeed);
            }
        }

        while (dialogueSoundIsPlaying) yield return new WaitForSeconds(0.05f); // wait for sound dialogue to finish
        
        // actions to take after the entire line has finished displaying
        //continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void HideChoices() 
    {
        foreach (GameObject choiceButton in choices) 
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags) // handle tags made with #(tag) in ink story
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags) 
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length > 3) 
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            string tagOption = "";
            if(splitTag.Length>2) tagOption = splitTag[2].Trim();
            
            // handle the tag
            switch (tagKey) 
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case AUDIO_TAG:
                    StartCoroutine(PlayAndWaitForClip(tagValue, tagOption));
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                           + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices) 
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice() 
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine) 
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            InputManager.GetInstance().RegisterSubmitPressed(); 
            ContinueStory();
        }
    }
    
    public Ink.Runtime.Object GetVariableState(string variableName) 
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null) 
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
    

    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    public void OnApplicationQuit() 
    {
        dialogueVariables.SaveVariables();
        JournalUpdate journal = GameObject.FindGameObjectWithTag("Player").GetComponent<JournalUpdate>();
        journal.restoreToDefaults();
        PlayerPrefs.SetFloat("Gamma", GameObject.Find("PauseMenu").GetComponent<BrightnessSliderBehavior>().brightnessSlider.value);
        Debug.Log("closing game with gamma player pref" + PlayerPrefs.GetFloat("Gamma", -1));
    }
    private IEnumerator PlayAndWaitForClip(string path, string volumeOption)
    {
        float volume = PlayerPrefs.GetFloat("GlobalVolume");
        if (volumeOption.Length > 0) volume = float.Parse(volumeOption);
        var dialogueVoice = path.Contains("DialogueVoice/");
        var music = path.Contains("Music/");
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        if (audioClip == null)
        {
            Debug.LogError("Resource not found: "+ path);
            yield break;
        }
        if (music)
        {
            BackgroundSoundManager.AudioSource.clip = audioClip;
            // BackgroundSoundManager.AudioSource.volume = volume;
            BackgroundSoundManager.AudioSource.volume = PlayerPrefs.GetFloat("GlobalVolume");
            BackgroundSoundManager.AudioSource.Play();
            yield return null;
        } 
        else if (dialogueVoice)
        {
            print("DialogueAudio: "+path);
            while (dialogueSoundIsPlaying) yield return new WaitForSeconds(0.3f);
            var aBitForwardsGO = new GameObject("tmpVoiceObj");
            aBitForwardsGO.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
            Destroy(aBitForwardsGO, audioClip.length); // crash if audioClip wasn't found
            var source = AudioUtility.CreateSFX(audioClip, aBitForwardsGO.transform, 1f, volume);
            dialogueSoundIsPlaying = true;
            while (source != null && source.isPlaying)
                yield return new WaitForSeconds(0.3f);
        }
        else
        {
            AudioUtility.CreateSFX(audioClip, transform, 0, volume);
        }
        dialogueSoundIsPlaying = false;
    }
}
