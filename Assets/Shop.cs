using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    public List<Item> shopInventory;

    public GameObject shopWindow;

    bool isFirstTime = true;

    DialogueManager instance;

    public override void Interact()
    {
        base.Interact();

        if (isFirstTime)
        {
            StartCoroutine("Dialogue");
        }
        else
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

        GameTime.instance.paused = true;

    }

}
