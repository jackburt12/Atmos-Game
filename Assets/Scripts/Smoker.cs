using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoker : Interactable
{
    Animator animator;

    private int dragInterval = 15;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        animator  = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        animator.SetFloat("Counter",animator.GetFloat("Counter") + Time.deltaTime);

        if(animator.GetFloat("Counter") > dragInterval) {
            animator.SetFloat("Counter", 0f);
        }
    }

    public override void Interact()
    {
        base.Interact();
        gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(offset);
    }
}
