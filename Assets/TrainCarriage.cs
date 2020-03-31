using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarriage : MonoBehaviour
{
    Animator[] doors;

    // Start is called before the first frame update
    void Start()
    {
        doors = gameObject.GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            foreach(Animator door in doors)
            {
                door.SetBool("Open", !door.GetBool("Open"));
            }
        }
    }
}
