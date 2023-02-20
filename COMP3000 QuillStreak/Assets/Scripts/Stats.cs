using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] Text statText;
    [SerializeField] Button getStatButton;
    [SerializeField] Slider[] sliders;
    [SerializeField] Dropdown dropdown;
    private bool doneFirst = false;
    private string currChar;
    
    private void Start()
    {
        PlayerPrefs.SetInt("RunDefault@@", 0);
        getPlayers();
        //getStats("Global");
    }
    private void Update()
    {
        if (!doneFirst) { getStats("Global"); doneFirst = true; }
    }

    public void getPlayers() 
    {
        string[] players = PlayerPrefs.GetString("ALLPLAYERS").Split(' ');
        for (int i = 0; i < players.Length; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = players[i];
            dropdown.options.Add(option);
        }
    }

    public void getStats(string name) 
    {
        for(int i = 0; i < sliders.Length; i++) 
        {
            sliders[i].maxValue = 1;
            sliders[i].value = 1;
        }

        statText.text = PlayerPrefs.GetString(name);
        
        char[] tempChars = statText.text.ToCharArray();
        for (int i = 0; i < tempChars.Length; i++)
        {
            tempChars[i] = char.ToUpper(tempChars[i]);
        }
        
        byte[] asciiBytes = System.Text.Encoding.ASCII.GetBytes(tempChars);
        
        for (int i = 0; i < asciiBytes.Length; i++)
        {
            asciiBytes[i] -= 65;
        }
        
        if(asciiBytes.Length == statText.text.Length) 
        {
            for (int i = 0; i < asciiBytes.Length; i++)
            {
                if (char.IsUpper(statText.text[i]) == true) { sliders[asciiBytes[i]].value++; sliders[asciiBytes[i]].maxValue++; }
                else if(char.IsLower(statText.text[i]) == true) { sliders[asciiBytes[i]].maxValue++; }
            }
            
            for (int i = 0; i < sliders.Length; i++)
            {
                sliders[i].maxValue--;
                sliders[i].value--;
            }
            
        }
        else 
        {
            statText.text = "Stats Corrupted, try again";
        }

    }
    public void getStats() 
    {
        getStats(dropdown.options[dropdown.value].text);
    }

    public void returnToMenu() 
    {
        SceneManager.LoadScene("Menu");
    }
       
    public void resetStats() 
    {
        if (dropdown.value == 0) 
        {
            resetAll();
            return;
        }
        string remove = PlayerPrefs.GetString(dropdown.options[dropdown.value].text);
        string global = PlayerPrefs.GetString("Global");
        global = global.Replace(remove, "");
        PlayerPrefs.SetString("Global", global);
        PlayerPrefs.SetString(dropdown.options[dropdown.value].text, "");
        getStats();
    }

    public void resetAll() 
    {
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            PlayerPrefs.SetString(dropdown.options[i].text, "");
        }
        PlayerPrefs.SetString("ALLPLAYERS", "Global ");
        dropdown.options.Clear();
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = "Global";
        dropdown.options.Add(option);
        dropdown.value = 0;
        getStats();

    }
}
