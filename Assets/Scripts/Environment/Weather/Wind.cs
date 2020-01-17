using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    ParticleSystem[] systems;

    // Start is called before the first frame update
    void Start()
    {
        systems = gameObject.GetComponentsInChildren<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {

        int randNum = Random.Range(0,100);
        if(randNum > 96) {

            foreach(ParticleSystem system in systems) {
                ParticleSystem.ForceOverLifetimeModule forceOverLifetime = system.forceOverLifetime;
                
                if(forceOverLifetime.x.constant < 5f) {
                    forceOverLifetime.x = forceOverLifetime.x.constant + 1f;
                }
            
            }

            
            
        } else if (randNum < 3) {

            foreach(ParticleSystem system in systems) {
                ParticleSystem.ForceOverLifetimeModule forceOverLifetime = system.forceOverLifetime;
                
                if(forceOverLifetime.x.constant > -5f) {
                    forceOverLifetime.x = forceOverLifetime.x.constant - 1f;
                }
            
            }
        }

    }

}
