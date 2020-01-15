using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{

    Sprite[] sprites;

    void Awake()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/World/Trash");

        int randNum = Random.Range(0,sprites.Length);
 
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[randNum];
    }

}
