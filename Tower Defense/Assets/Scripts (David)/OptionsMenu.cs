using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class OptionsMenu : MonoBehaviour
{
    public AudioManager audioManager;
    public Slider sliderMusic;
    // Start is called before the first frame update
    void Start()
    {
        sliderMusic.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        audioManager.changeVolume(sliderMusic.value);
    }
}
