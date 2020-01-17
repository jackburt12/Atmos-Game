using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{

    public bool paused = false;

    public static GameTime instance;

    //How many times faster in game passes than real time.
    public int timeScale = 1000;

    public int currentMinutes, currentHours;
    public float currentSeconds;

    void Awake()
    {
         if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //What time should the game start at on the first boot?
        currentSeconds = 0;
        currentMinutes = 0;
        currentHours = 7;

    }

    // Update is called once per frame
    void Update()
    {
        if (!paused) {
            
            UpdateTime();

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
        }

        if (currentHours > 23) {
            currentHours = 0;
        }

        GameObject.Find("Time").GetComponent<Text>().text = "Time: " + PrintTime();

    }

    string PrintTime() {

        return currentHours.ToString("00") + ":" + currentMinutes.ToString("00");

    }

}
