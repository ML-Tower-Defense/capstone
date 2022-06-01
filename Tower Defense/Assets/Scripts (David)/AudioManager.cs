using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    private bool golemFound;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start () {
        Play("BackgroundMusic");

        InvokeRepeating("checkForGolem", 0f, 2);
    }

    private void checkForGolem()
    {
        if (GameObject.Find("GolemEnemyPrefab(Clone)") && !golemFound)
        {
            golemFound = true;

            sounds[0].source.Stop();
            sounds[0].source.volume = 0.4f;
            sounds[0].source.pitch = 0.6f;

            Play("BackgroundMusic");
        }
    }

    // Update is called once per frame
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            return;
        }
        s.source.Play();
    }

    public void changeVolume (float i){
       foreach (Sound s in sounds) {
            if (s.name == "BackgroundMusic" || s.name == "WinGame" || s.name == "LoseGame") {
                s.source.volume = i;
            }
        }
    }

    public void StopAllAudio () {
        foreach (Sound s in sounds){
            s.source.Stop();
        }
    }

    public void changeSFX (float i) {
        foreach (Sound s in sounds) {
            if (s.name != "BackgroundMusic" && s.name != "WinGame" && s.name != "LoseGame") {
                s.source.volume = i;
            }
        }
    }

}
