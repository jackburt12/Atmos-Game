using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    public List<Item> shopInventory;

    public GameObject shopWindow;

    bool isFirstTime = true;

    bool dialogueFinished = false;

    DialogueManager instance;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public override void Interact()
    {
        base.Interact();

        if(isFirstTime)
        {
            StartCoroutine("Dialogue");
        } else
        {
            OpenShop();
        }

        
    }

    IEnumerator Dialogue ()
    {
        gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(offset);
        isFirstTime = false;

        instance = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        while (instance.isSpeaking)
        {
            yield return null;
        }

        OpenShop();
    }

    public void OpenShop()
    {
        
        shopWindow.GetComponent<ShopInventory>().PopulateShop(shopInventory);
        shopWindow.SetActive(true);

    }

}
