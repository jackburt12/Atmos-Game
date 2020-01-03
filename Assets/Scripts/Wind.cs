using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    ParticleSystem system;
    ParticleSystem.ForceOverLifetimeModule forceOverLifetime;

    // Start is called before the first frame update
    void Start()
    {
        system = gameObject.GetComponent<ParticleSystem>();
        forceOverLifetime = system.forceOverLifetime;


    }

    // Update is called once per frame
    void Update()
    {

        int randNum = Random.Range(0,100);
        if(randNum > 96) {
            if(forceOverLifetime.x.constant < 5f) {
                forceOverLifetime.x = forceOverLifetime.x.constant + 1f;
            }
            
        } else if (randNum < 3) {
            if(forceOverLifetime.x.constant > -5f) {
                forceOverLifetime.x = forceOverLifetime.x.constant - 1f;
            }
        }

    }

}
