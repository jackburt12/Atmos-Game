using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;


public class AudioManager : MonoBehaviour
{

    AudioSource musicPlayer;

    public Sound[] sounds;
    public Sound[] music;

    public int masterVolume = 10;
    public int soundVolume = 10;
    public int musicVolume = 10;

    public static AudioManager instance;

    public bool musicEnabled = true;

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

        musicPlayer = gameObject.AddComponent<AudioSource>();
        musicPlayer.pitch = 1f;


        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * 0.01f * soundVolume * masterVolume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        StartCoroutine("PlayMusic");
        
    }

    public void Play(string name) { 
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null) {
            Debug.Log("Count not find sound with name.");
            return;
        }
        s.source.Play();

    }

    public void StopPlaying (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null) {
            Debug.Log("Count not find sound with name.");
            return;
        }
        s.source.Stop();
    }

    public IEnumerator FadePlay(string name) {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null) {
            Debug.Log("Count not find sound with name.");
            yield break;
        }


        s.source.volume = 0f;
        s.source.Play();
        
        for (float f = 0f; f < s.volume; f+=0.001f) {
            s.source.volume = f;
            yield return null;
        }

    }

    public IEnumerator FadeStopPlaying(string name) {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null) {
            Debug.Log("Count not find sound with name.");
            yield break;
        }


        s.source.volume = s.volume;
        
        for (float f = s.volume; f > 0; f-=0.001f) {
            s.source.volume = f;
            yield return null;
        }

        s.source.Stop();


    }

    public IEnumerator PlayMusic() {

        int count = 0;
        
        while(musicEnabled) {
            Debug.Log("Song number: " + count);
            Sound song = music[count];

            musicPlayer.volume = song.volume * 0.01f * musicVolume * masterVolume;
            musicPlayer.clip = song.clip;

            musicPlayer.Play();

            while(musicPlayer.isPlaying) {
                yield return null;
            }

            count++;
            if(count >= music.Length) {
                count = 0; 
            }

            yield return null;
        }

    }

}
