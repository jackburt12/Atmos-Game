using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private int charCountLimit = 20;
    
    public static DialogueManager instance;

    private GameTime gameTime;

    public Text nameText, dialogueText;

    private Queue<string> sentences;

    private GameObject dialoguePopup;

    private bool isSpeaking = false;

    private DialogueTrigger context;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        gameTime = GameObject.Find("GameManager").GetComponent<GameTime>();


        sentences = new Queue<string>();
    }

    void Update() {
        if(isSpeaking) {
            if(Input.GetButtonDown(("Interact"))) {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue, GameObject popup, DialogueTrigger trigger) {

        DialogueCameraOn();

        isSpeaking = true;

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
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        } else {
            string sentence = sentences.Dequeue();
            StopCoroutine("DisplayNextSentence");
            StartCoroutine(TypeSentance(sentence));
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
            Debug.Log(f);
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
        GameObject.Find("Black Bars").GetComponent<BlackBars>().MoveBarsIn();
    }

    void DialogueCameraOff() {
        gameTime.paused = false;
        GameObject.Find("Black Bars").GetComponent<BlackBars>().MoveBarsOut();
    }
}
