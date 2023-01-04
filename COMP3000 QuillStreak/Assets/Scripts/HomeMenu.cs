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
    public Text NameText;
    public InputField PlayerName;
    public Slider DiffSlider;
    public Slider WordSlider;
    public Image Blank;
    public Sprite Check;
    public Sprite Cross;
    public Sprite Cirlce;
    private int words = 4;
    private int diff = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        PickEng();
        PlayerPrefs.SetInt("Lives", 6);
        PlayerPrefs.SetInt("Difficulty", diff);
        PlayerPrefs.SetInt("WordCount", words);
        
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
        if (Found == true)
        {
            NameText.text = "Welcome Back!";
            Blank.sprite = Check;
            PlayerPrefs.SetString("currPlayer", PlayerName.text);
        }
        else if (Found == false)
        {
            NameText.text = "New Player Added!";
            Blank.sprite = Cirlce;
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
        nameChange();
        SceneManager.LoadScene("Castle");
    }
    public void exitGame() 
    {
        Application.Quit();
    }
    public void viewStats() 
    {
        SceneManager.LoadScene("StatsPage");
    }
    private bool A = false;
    private bool d = false;
    private bool u = false;
    private bool l = false;
    private bool t = false;

}
