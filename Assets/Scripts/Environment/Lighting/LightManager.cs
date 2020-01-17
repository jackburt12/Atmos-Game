using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightManager : MonoBehaviour
{

    public static LightManager instance;

    GameTime gameTime;

    Color globalNight = new Color(0.7f, 0.75f, 1f, 1f);
    Color globalDay = new Color(1f,1f,1f,1f);
    float globalNightIntensity = 0.5f;
    float globalDayIntensity = 1.5f;


    UnityEngine.Experimental.Rendering.LWRP.Light2D globalLight;

    bool night = false;

    //the time at which it turns to night
    int nightTime = 19;
    //the time at which it turns to day
    int dayTime = 7;


    GameObject[] lights;

    Dictionary<UnityEngine.Experimental.Rendering.LWRP.Light2D, float> lightMap = new Dictionary<UnityEngine.Experimental.Rendering.LWRP.Light2D, float>();


    // Start is called before the first frame update
    void Awake()
    {

         if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        gameTime = GameObject.Find("GameManager").GetComponent<GameTime>();

        globalLight = gameObject.GetComponent<UnityEngine.Experimental.Rendering.LWRP.Light2D>();
        lights = GameObject.FindGameObjectsWithTag("NightLight");

        foreach (GameObject light in lights) {
            UnityEngine.Experimental.Rendering.LWRP.Light2D light2D = light.GetComponent<UnityEngine.Experimental.Rendering.LWRP.Light2D>();
            lightMap.Add(light2D, light2D.intensity);
            light2D.intensity=0f;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if(night) {
            if(gameTime.currentHours >= dayTime && gameTime.currentHours < nightTime) {
                Debug.Log("It's daytime.");
                night = false;
                StartCoroutine("StartDaytime");
            }
        } else {
            if(gameTime.currentHours >= nightTime || gameTime.currentHours < dayTime) {
                Debug.Log("It's nighttime.");
                night=true;
                StartCoroutine("StartNighttime");
            }
        }
        
        

    }

    IEnumerator StartDaytime() {

        float elapsedTime = 0f;
        float totalTime = 6f;

        while (elapsedTime < totalTime) {
            elapsedTime += Time.deltaTime;
            float time = elapsedTime/totalTime;
            
            globalLight.color = Color.Lerp(globalNight, globalDay, time);
            globalLight.intensity = Mathf.Lerp(globalNightIntensity, globalDayIntensity, time);
            
            foreach (var light in lightMap) {
                light.Key.intensity = Mathf.Lerp(light.Value, 0f, time);
            }

            yield return null;
        }

    }

    IEnumerator StartNighttime() {

        float elapsedTime = 0f;
        float totalTime = 6f;

        while (elapsedTime < totalTime) {
            elapsedTime += Time.deltaTime;
            float time = elapsedTime/totalTime;
            globalLight.color = Color.Lerp(globalDay, globalNight, time);
            globalLight.intensity = Mathf.Lerp(globalDayIntensity, globalNightIntensity, time);
            
            foreach (var light in lightMap) {
                light.Key.intensity = Mathf.Lerp(0f, light.Value, time);
            }

            yield return null;
        }

    }

}
