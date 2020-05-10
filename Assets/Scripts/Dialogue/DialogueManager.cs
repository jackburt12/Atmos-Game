using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class DialogueManager : MonoBehaviour
{
    private int charCountLimit = 20;
    
    public static DialogueManager Instance;

    private GameTime gameTime;

    public Text nameText, dialogueText;

    private Queue<string> sentences;

    private GameObject dialoguePopup;

    public bool isSpeaking = false;
    private bool frameBuffer = false;

    private DialogueTrigger context;

    private List<Coroutine> coroutineList;

    CinematicBars bars;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        gameTime = GameObject.Find("GameManager").GetComponent<GameTime>();
        bars = GameObject.Find("CinematicBars").GetComponent<CinematicBars>();

        sentences = new Queue<string>();

        isSpeaking = false;
    }

    void Update() {

        if(isSpeaking) {
            if(frameBuffer == false)
            {
                frameBuffer = true;
            } else
            {
                if (Input.GetButtonDown(("Interact")))
                {
                    //Debug.Log("Next sentence pls");
                    DisplayNextSentence();
                }
            } 
        }
    }

    public void StartDialogue(Dialogue dialogue, GameObject popup, DialogueTrigger trigger) {

        DialogueCameraOn();

        dialoguePopup = popup;
        context = trigger;

        StartCoroutine("DialoguePromptFadeIn");

        nameText = dialoguePopup.GetComponentsInChildren<Text>()[0];
        dialogueText = dialoguePopup.GetComponentsInChildren<Text>()[1];
        
        sentences.Clear();

        nameText.text = dialogue.name;

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        coroutineList = new List<Coroutine>();

        DisplayNextSentence();

    }

    public void DisplayNextSentence() {

        if(sentences.Count == 0) {
            EndDialogue();
            return;
        } else {
            isSpeaking = true;

            string sentence = sentences.Dequeue();

            foreach (Coroutine c in coroutineList) {
                StopCoroutine(c);
            }
            coroutineList.Add(StartCoroutine(TypeSentance(sentence)));
        }
    }

    IEnumerator TypeSentance (string sentence) {
        int charCount = 0;

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            if(char.IsWhiteSpace(letter) && charCount>=charCountLimit) {
                dialogueText.text+= "\n";
                charCount = 0;
            } else {
                dialogueText.text += letter;
                charCount ++;
            }
        
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForEndOfFrame();
        }
    }

    void EndDialogue() {

        isSpeaking = false;
        StartCoroutine("DialoguePromptFadeOut");
        DialogueCameraOff();

    }

    IEnumerator DialoguePromptFadeIn()
    {
        for (float f = 0.0f; f <= 1f; f += 0.05f)
        {
            dialoguePopup.GetComponent<CanvasGroup>().alpha = f;
            yield return null;
        }
    }

    IEnumerator DialoguePromptFadeOut()
    {
        for (float f = 1f; f >= 0f; f -= 0.05f)
        {
            dialoguePopup.GetComponent<CanvasGroup>().alpha = f;
            yield return null;
        }

        dialoguePopup.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void DialogueCameraOn() {
        gameTime.paused = true;
        bars.Show(Screen.height/4,0.4f);
        bars.StartCoroutine("ZoomIn");
       
    }

    void DialogueCameraOff() {
        gameTime.paused = false;
        bars.Hide(0.4f);
        bars.StartCoroutine("ZoomOut");
    }
}
