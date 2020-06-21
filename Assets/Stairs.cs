using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public int stairHeight;
    private GameObject[] stairs;

    public GameObject stairPrefab;

    // Start is called before the first frame update
    void Start()
    {
        stairs = new GameObject[stairHeight];

       for (int i = 0; i < stairHeight; i++)
        {
            stairs[i] =  GameObject.Instantiate(stairPrefab);
            stairs[i].transform.position = gameObject.transform.position + new Vector3(i, -i, 0);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
