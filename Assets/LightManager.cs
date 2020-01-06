using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightManager : MonoBehaviour
{

    public static LightManager instance;

    

    //How many times faster in game passes than real time.
    int timeScale = 1000;

    int currentMinutes, currentHours;
    float currentSeconds;

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

        currentSeconds = 0;
        currentMinutes = 0;
        currentHours = 0;

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

        UpdateTime();

        if(night) {
            if(currentHours >= dayTime && currentHours < nightTime) {
                Debug.Log("It's daytime.");
                night = false;
                StartCoroutine("StartDaytime");
            }
        } else {
            if(currentHours >= nightTime || currentHours < dayTime) {
                Debug.Log("It's nighttime.");
                night=true;
                StartCoroutine("StartNighttime");
            }
        }
        
        

    }

    void UpdateTime() {

        currentSeconds += (Time.deltaTime * timeScale);

        if (currentSeconds >= 60) {
            currentMinutes += 1;
            currentSeconds -= 60;

        }

        if (currentMinutes > 59) {
            currentHours += 1;
            currentMinutes -= 60;
            Debug.Log(PrintTime());
        }

        if (currentHours > 23) {
            currentHours = 0;
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

    string PrintTime() {

        return currentHours.ToString("00") + ":" + currentMinutes.ToString("00");

    }

}
