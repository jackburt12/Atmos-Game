using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private static bool isPaused = false;

    public GameObject pauseMenu, settingsMenu, audioMenu, displayMenu, controlsMenu;

    private GameTime gameTime;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = GameObject.Find("GameManager").GetComponent<GameTime>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {

            if (isPaused) {

                if (pauseMenu.activeSelf) {
                    Resume();
                } else if (settingsMenu.activeSelf) {
                    settingsMenu.SetActive(false);
                    pauseMenu.SetActive(true);
                } else if (audioMenu.activeSelf) {
                    audioMenu.SetActive(false);
                    settingsMenu.SetActive(true);
                } else if (displayMenu.activeSelf) {
                    displayMenu.SetActive(false);
                    settingsMenu.SetActive(true);
                } else if (controlsMenu.activeSelf) {
                    controlsMenu.SetActive(false);
                    settingsMenu.SetActive(true);
                }

            } else {
                Pause();
            }
        }
    }

    void Pause() {
        isPaused = true;
        gameTime.paused = true;
        pauseMenu.SetActive(true);
    }

    void Resume() {
        isPaused = false;
        gameTime.paused = false;
        pauseMenu.SetActive(false);
    }
}
