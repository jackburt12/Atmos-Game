using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager instance;
    
    public Dialogue dialogue;

    private GameObject dialoguePrefab;
    private GameObject dialoguePopup;

    void Awake() {
        //instance = DialogueManager.instance;
        instance = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    void Start() {
        dialoguePrefab = Resources.Load("Prefabs/UI/Dialogue") as GameObject;

        Vector2 whereToInstantiate = new Vector2(transform.position.x, transform.position.y + 1.5f);
        dialoguePopup = Instantiate(dialoguePrefab, transform);
        dialoguePopup.transform.position = whereToInstantiate;
        dialoguePopup.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void TriggerDialogue() {
        Debug.Log("Speaking to " + gameObject.name);
        instance.StartDialogue(dialogue, dialoguePopup, this);
    }
}
