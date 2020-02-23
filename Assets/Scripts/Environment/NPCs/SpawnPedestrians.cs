using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPedestrians : MonoBehaviour
{
    //how many pedestrians should spawn
    //a spawn rate of 1 means one every 10 seconds
    public int spawnRate;

    GameObject[] pedestriansList;
    GameObject pedestrian;
    public GameObject player;

    float rate;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        pedestriansList = Resources.LoadAll<GameObject>("Prefabs/Characters/Pedestrians");

        rate = 10 / spawnRate;
        timer = rate;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnPedestrian();
            timer = rate;
        }
    }

    void SpawnPedestrian() {

        pedestrian = pedestriansList[(int)Random.Range(0,pedestriansList.Length)];


        int rand = Random.Range(0, 2);
        if(rand == 0) {
            rand = -1;
        }

        Vector3 positionVector = new Vector3((player.transform.position.x + rand * 15), -3, player.transform.position.z);
        GameObject spawnedPedestrian = Instantiate(pedestrian, positionVector, Quaternion.identity, gameObject.transform);
        spawnedPedestrian.transform.parent = gameObject.transform.parent;

        if(rand<0) {
            spawnedPedestrian.GetComponent<Pedestrian>().goLeft = false;
        }

    }

}
