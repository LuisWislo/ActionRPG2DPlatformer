using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private AudioManager audio;

    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        audio.Play("SettingsSong");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
