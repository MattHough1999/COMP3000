using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEditor;
public class HomeMenu : MonoBehaviour
{
    public Button secret;
    public Text Difftext;
    public Text WordText;
    public Slider DiffSlider;
    public Slider WordSlider;
    private int words = 4;
    private int diff = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        PickEng();

        PlayerPrefs.SetInt("Difficulty", diff);
        PlayerPrefs.SetInt("WordCount", words);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || A == true)
            {
            Debug.Log("a");
                A = true;
            if (Input.GetKeyDown(KeyCode.D) || d == true)
            {
                Debug.Log("d");
                d = true;
                if (Input.GetKeyDown(KeyCode.U) || u == true)
                {
                    Debug.Log("u");
                    u = true;
                    if (Input.GetKeyDown(KeyCode.L) || l == true)
                    {
                        Debug.Log("l");
                        l = true;
                        if (Input.GetKeyDown(KeyCode.T) || t == true)
                        {
                            Debug.Log("t");
                            t = true;
                            secret.interactable = true;
                            secret.image.enabled = true;
                            //secret.enabled = true;

                        }
                    }
                }
            }
        }
    }
    
    public void diffChange() 
    {
        diff = (int)DiffSlider.value;
        Difftext.text = "Difficulty: " + diff;
        PlayerPrefs.SetInt("Difficulty", diff);
    }
    public void wordChange()
    {
        words = (int)WordSlider.value;
        WordText.text = "Word Count: " + words;
        PlayerPrefs.SetInt("WordCount", words);
    }
    public void PickEng() 
    {
        PlayerPrefs.SetString("SelectedDictionary", "Dictionary");
    }
    public void PickRU()
    {
        PlayerPrefs.SetString("SelectedDictionary", "DictionaryRU");
    }
    public void PickSecret() 
    {
        PlayerPrefs.SetString("SelectedDictionary", "filter");
    }
    public void loadSpace() 
    {
        SceneManager.LoadScene("Space");
    }
    public void loadForest()
    {
        SceneManager.LoadScene("Forest");
    }
    public void loadWest()
    {
        SceneManager.LoadScene("WildWest");
    }
    public void loadCastle()
    {
        SceneManager.LoadScene("Castle");
    }
    private bool A = false;
    private bool d = false;
    private bool u = false;
    private bool l = false;
    private bool t = false;

}
