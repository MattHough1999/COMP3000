using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] Slider MVol, AVol, EVol;
    [SerializeField] Image tick;
    [SerializeField] AudioSource ambientSource, effSource;
    [SerializeField] AudioClip testClip;
    public float mvol = 0.5f, avol = 0.5f, evol = 0.5f;
    float timer = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        mvol = PlayerPrefs.GetFloat("Volume",0.5f);
        evol = PlayerPrefs.GetFloat("EffVolume",0.5f);
        avol = PlayerPrefs.GetFloat("AmVolume",0.5f);
        MVol.value = mvol;
        EVol.value = evol;
        AVol.value = avol;
        tick.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        if (timer <=0) { tick.enabled = false; }
        ambientSource.volume = mvol * avol;
        effSource.volume = mvol * evol;
    }
    public void save() 
    {
        effSource.PlayOneShot(testClip);
        tick.enabled = true;
        timer = 2.0f;
    }

    public void volChange()
    {
        mvol = MVol.value;
        PlayerPrefs.SetFloat("Volume",mvol);
    }
    public void effVolChange()
    {
        evol = EVol.value;
        PlayerPrefs.SetFloat("EffVolume", evol);
        //Debug.Log (evol);
    }
    public void amVolChange()
    {
        avol = AVol.value;
        PlayerPrefs.SetFloat("AmVolume", avol);
    }
    public void testEffectVol() 
    {
        effSource.PlayOneShot(testClip);
    }

    public void changeScene(string scene) 
    {
        SceneManager.LoadScene(scene);
    }
    public void exitGame() 
    {
        Application.Quit();
    }
}
