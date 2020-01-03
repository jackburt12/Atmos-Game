using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;


public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

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

        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
    }

    public void Play(string name) { 
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null) {
            Debug.Log("Count not find sound with name.");
            return;
        }
        s.source.Play();

    }

    public IEnumerator FadePlay(string name) {

        Debug.Log("Playing sound with name: " + name);

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null) {
            Debug.Log("Count not find sound with name.");
            yield break;
        }


        s.source.volume = 0f;
        s.source.Play();
        
        for (float f = 0f; f < s.volume; f+=0.0008f) {
            s.source.volume = f;
            yield return null;
        }

    }

}
