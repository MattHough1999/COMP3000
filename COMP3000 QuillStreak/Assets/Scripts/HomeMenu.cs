using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEditor;
public class HomeMenu : MonoBehaviour
{
    public Button secret;
    public Text Difftext, WordText, NameText;
    public InputField PlayerName;
    public Slider DiffSlider, WordSlider;
    public Image Blank;
    public Sprite Check, Cross, Circle;
    private int words = 4;
    private int diff = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume", 0.5f) * PlayerPrefs.GetFloat("AmVolume", 0.5f); 
        PickEng();
        PlayerPrefs.SetInt("Lives", 6);
        PlayerPrefs.SetInt("Difficulty", diff);
        PlayerPrefs.SetInt("WordCount", words);
        PlayerPrefs.SetString("OverText", "This is the stats page\nPick your name to see your stats!");
        
        PlayerPrefs.SetString("currPlayer", "Global");
        string players = PlayerPrefs.GetString("ALLPLAYERS");
        if (!players.Contains("Global")) { PlayerPrefs.SetString("ALLPLAYERS", "Global "); PlayerPrefs.SetString("Global", ""); }
        
    }

    public void diffChange() 
    {
        diff = (int)DiffSlider.value;
        Difftext.text = "Difficulty: " + (diff - 2);
        PlayerPrefs.SetInt("Difficulty", diff);

    }
    public void wordChange()
    {
        words = (int)WordSlider.value;
        WordText.text = "Word Count: " + (words + 1);
        PlayerPrefs.SetInt("WordCount", words);
    }
    
    public void nameChange() 
    {
        PlayerPrefs.SetString("currPlayer", PlayerName.text);

        bool Found = false;
        string allPlayers = PlayerPrefs.GetString("ALLPLAYERS");
        string[] ArrPlayers = allPlayers.Split(' ');

        for (int i = 0; i < ArrPlayers.Length; i++)
        {
            if (ArrPlayers[i] == PlayerName.text)
            {
                Found = true;
            }
        }
        if (Found)
        {
            NameText.text = "Welcome Back!";
            Blank.sprite = Check;
            PlayerPrefs.SetString("currPlayer", PlayerName.text);
        }
        else if (!Found)
        {
            NameText.text = "New Player Added!";
            Blank.sprite = Circle;
            PlayerPrefs.SetString("ALLPLAYERS", allPlayers + PlayerName.text + ' ');
            PlayerPrefs.SetString("currPlayer", PlayerName.text);
        }
        else 
        {
            NameText.text = "Something went wrong! Please try again...";
            Blank.sprite = Cross;
        }
    }
    
    public void PickEng() 
    {
        PlayerPrefs.SetInt("RunDefault@@", 1);
        PlayerPrefs.SetString("SelectedDictionary", "Dictionary");
    }
    public void PickRU()
    {
        PlayerPrefs.SetInt("RunDefault@@", 0);
        PlayerPrefs.SetString("SelectedDictionary", "DictionaryRU");
    }
    public void PickSecret() 
    {
        PlayerPrefs.SetInt("RunDefault@@", 1);
        PlayerPrefs.SetString("SelectedDictionary", "filter");
    }

    public void loadScene(string scene) 
    {

        Debug.Log("clicked");
        SceneManager.LoadScene(scene);
    }
    public void exitGame() 
    {
        Application.Quit();
    }
    
    private bool A = false;
    private bool d = false;
    private bool u = false;
    private bool l = false;
    private bool t = false;

}
