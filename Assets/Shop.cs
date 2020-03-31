using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    List<Item> shopInventory;

    bool isFirstTime = true;

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
            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(offset);
        }

        GameObject.Find("Shop Window").SetActive(true);
    }
}
