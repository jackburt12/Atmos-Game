using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{

    public int numberOfCarriages;

    public GameObject endCarriagePrefab, middleCarriagePrefab;

    private List<GameObject> carriages;
    // Start is called before the first frame update
    void Start()
    {
        if (numberOfCarriages < 2)
        {
            numberOfCarriages = 2;
        }

        int[] indexes = new int[numberOfCarriages];

        for (int i = 0; i < numberOfCarriages; i++)
        {
            indexes[i] = -numberOfCarriages / 2 + i;
        }

        carriages = new List<GameObject>();

        carriages.Add(Instantiate(endCarriagePrefab, transform));

        for (int i = 1; i < numberOfCarriages - 1; i++)
        {
            carriages.Add(Instantiate(middleCarriagePrefab, transform));
        }
        carriages.Add(Instantiate(endCarriagePrefab, transform));

        for (int i = 0; i < numberOfCarriages; i++) {
            Debug.Log(indexes[i] * 10);
            carriages[i].transform.position = new Vector3(indexes[i] * 10, carriages[i].transform.position.y);
        }

        carriages[0].transform.localScale = new Vector3(-1f, 1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
