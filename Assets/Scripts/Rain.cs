using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{

    ParticleSystem system;
    ParticleSystem.EmissionModule emission;
    ParticleSystem.SizeOverLifetimeModule size;

    bool isRaining = false;
    int rainType = -1;

    // Start is called before the first frame update
    void Start()
    {

        system = gameObject.GetComponent<ParticleSystem>();
        emission = system.emission;

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space)) {
            RainIntensity intensity = (RainIntensity)Random.Range(0,5);
            StartCoroutine("StartRain", intensity);
        }

        if(isRaining) {
            if(rainType == 2) {
                int randNum = Random.Range(0,10000);
                if(randNum == 1) {
                    FindObjectOfType<AudioManager>().Play("DistantThunder");
                }
            } else if (rainType == 3) {
                int randNum = Random.Range(0,10000);
                if(randNum < 2) {
                    FindObjectOfType<AudioManager>().Play("DistantThunder");

                }
                else if(randNum < 4 ) {
                    int fiftyfifty = Random.Range(0,2);
                    if(fiftyfifty==0) {
                        FindObjectOfType<AudioManager>().Play("MidThunder1");

                    } else {
                        FindObjectOfType<AudioManager>().Play("MidThunder2");
                    }
                } else if(randNum == 4) {
                    int fiftyfifty = Random.Range(0,2);
                    if(fiftyfifty==0) {
                        FindObjectOfType<AudioManager>().Play("HeavyThunder1");
                    } else {
                        FindObjectOfType<AudioManager>().Play("HeavyThunder2");
                    }
                }
            } else if (rainType == 4) {
                int randNum = Random.Range(0,10000);
                if(randNum < 3) {
                    FindObjectOfType<AudioManager>().Play("DistantThunder");

                }
                else if(randNum < 6 ) {
                    int fiftyfifty = Random.Range(0,2);
                    if(fiftyfifty==0) {
                        FindObjectOfType<AudioManager>().Play("MidThunder1");

                    } else {
                        FindObjectOfType<AudioManager>().Play("MidThunder2");

                    }
                } else if(randNum < 8) {
                    int fiftyfifty = Random.Range(0,2);
                    if(fiftyfifty==0) {
                        FindObjectOfType<AudioManager>().Play("HeavyThunder1");
                    } else {
                        FindObjectOfType<AudioManager>().Play("HeavyThunder2");
                    }
                }
            }
        }
        
    }

    IEnumerator StartRain( RainIntensity intensity ) {
       
        string audioName = "";

        if ((int)intensity <=1) { audioName = "LightRain"; }
        else if ((int)intensity >= 3) { audioName = "HeavyRain"; }
        else { audioName = "MediumRain"; }

        isRaining = true;
        rainType = (int)intensity;
        
        //play sound (fading in)
        FindObjectOfType<AudioManager>().StartCoroutine("FadePlay",audioName);

        for(float f = 0; f < ((int)intensity*100 + 50); f+=0.5f) {

            emission.rateOverTime = f;
            yield return null;
        }

    }

    void OnParticleCollision(GameObject other) {
        Debug.Log("It collided");
        //other.GetComponent<ParticleSystem>().sizeOverLifetime.x = 1f;
    }

}

enum RainIntensity {
    SPITTING = 0,
    LIGHT = 1,
    NORMAL = 2,
    HEAVY = 3,
    TORRENTIAL = 4
}