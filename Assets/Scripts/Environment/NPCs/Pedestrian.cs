using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Pedestrian : MonoBehaviour
{

    public bool goLeft = true;

    float movespeed;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        movespeed = 0.04f;
        float randNum = Random.Range(-0.01f,0.01f);
        movespeed = movespeed + (randNum);

        if(goLeft) {
            transform.localScale = new Vector3(-1,1,1);
            movespeed = -movespeed;
        }

        int randOrder = (int)Random.Range(-5,3);
        if(randOrder == 0) {
            randOrder = -1;
        }

        gameObject.GetComponent<SortingGroup>().sortingOrder = randOrder;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + movespeed, transform.position.y);
        
        float distance = player.transform.position.x - gameObject.transform.position.x;
        if(Mathf.Abs(distance) > 20f) {
            Destroy(gameObject);
        }
        
    }
}
