using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    private static bool isPaused = false;

    public GameObject pauseMenu, settingsMenu, audioMenu, displayMenu, controlsMenu;

    private GameTime gameTime;

    private AudioManager audioManager;

    public Slider masterVolumeSlider, soundVolumeSlider, musicVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = GameObject.Find("GameManager").GetComponent<GameTime>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !GameObject.Find("Shop Window").activeInHierarchy) {

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

    public void Resume() {
        isPaused = false;
        gameTime.paused = false;
        pauseMenu.SetActive(false);
    }

    public void MasterVolume() {

        audioManager.masterVolume = (int)masterVolumeSlider.value;
        audioManager.UpdateVolume();

    }


    public void SoundVolume() {

        audioManager.soundVolume = (int)soundVolumeSlider.value;
        audioManager.UpdateVolume();

    }

    public void MusicVolume() {

        audioManager.musicVolume = (int)musicVolumeSlider.value;
        audioManager.UpdateVolume();

    }
}
