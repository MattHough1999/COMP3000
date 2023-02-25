using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] AudioSource ambientSource;
    [SerializeField] AudioSource effectSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void sceneStart(bool keptPos) 
    {
        ambientSource.volume = PlayerPrefs.GetFloat("AmbientVolume") * PlayerPrefs.GetFloat("MasterVolume") ;
        effectSource.volume = PlayerPrefs.GetFloat("EffectVolume") * PlayerPrefs.GetFloat("MasterVolume") ;
        if (keptPos) 
        {
            ambientSource.time = PlayerPrefs.GetFloat("AmbientPosition");
        }

    }
    public void sceneEnd(bool keepPos) 
    {
        if (keepPos) 
        {
            PlayerPrefs.SetFloat("AmbientVolume",ambientSource.time);
        }
    }
}
