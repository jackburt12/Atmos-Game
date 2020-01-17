using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rain : MonoBehaviour
{

    ParticleSystem[] systems;
    ParticleSystem.EmissionModule[] emissions;

    bool isRaining = false;
    int rainType = -1;

    //in seconds how short could the longest rainfall be or delay between rainfalls
    int minRainIntevals = 30;
    float currentRainTime = 0;

    RainIntensity currentRainIntensity;

    GameObject weatherUI;

    // Start is called before the first frame update
    void Start()
    {
        systems = gameObject.GetComponentsInChildren<ParticleSystem>();

        weatherUI = GameObject.Find("Weather");

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        currentRainTime += (Time.deltaTime);


        if(!isRaining) {
            int randNum2 = Random.Range(0,1000);
                if(randNum2 == 1 && currentRainTime >= minRainIntevals) {
                    currentRainTime = 0;
                    RainIntensity intensity = (RainIntensity)Random.Range(0,5);
                    StartCoroutine("StartRain", intensity);
                }
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

            int randNum3 = Random.Range(0,1000);
                if(randNum3 == 1 && currentRainTime >= minRainIntevals) {
                    currentRainTime = 0;
                    RainIntensity intensity = (RainIntensity)Random.Range(0,5);
                    StartCoroutine("StopRain", intensity);
                }
        }
        
    }

    IEnumerator StartRain( RainIntensity intensity ) {

        Debug.Log("It has started raining: " + intensity.ToString());

        weatherUI.GetComponent<Text>().text = "Weather: " + intensity.ToString() + " RAIN";

        currentRainIntensity = intensity;

        string audioName = "";

        if ((int)intensity <=1) { audioName = "LightRain"; }
        else if ((int)intensity >= 3) { audioName = "HeavyRain"; }
        else { audioName = "MediumRain"; }

        isRaining = true;
        rainType = (int)intensity;
        
        //play sound (fading in)
        FindObjectOfType<AudioManager>().StartCoroutine("FadePlay",audioName);

        for(float f = 0; f < ((int)intensity*100 + 50)/4; f+=0.25f) {

            foreach (ParticleSystem system in systems) {
                ParticleSystem.EmissionModule emission = system.emission;
                emission.rateOverTime = f;
            }
            yield return null;
        }

    }

    IEnumerator StopRain() {
        
        weatherUI.GetComponent<Text>().text = "Weather: CLEAR";

        string audioName = "";

        if ((int)currentRainIntensity <=1) { audioName = "LightRain"; }
        else if ((int)currentRainIntensity >= 3) { audioName = "HeavyRain"; }
        else { audioName = "MediumRain"; }

        isRaining = false;
        rainType = (int)currentRainIntensity;
        
        //stop sound (fading out)
        FindObjectOfType<AudioManager>().StartCoroutine("FadeStopPlaying",audioName);

        for(float f = ((int)currentRainIntensity*100 + 50)/4; f >= 0f; f-=0.25f) {

            foreach (ParticleSystem system in systems) {
                ParticleSystem.EmissionModule emission = system.emission;
                emission.rateOverTime = f;
            }
            yield return null;
        }

    }

}

enum RainIntensity {
    SPITTING = 0,
    LIGHT = 1,
    NORMAL = 2,
    HEAVY = 3,
    TORRENTIAL = 4
}