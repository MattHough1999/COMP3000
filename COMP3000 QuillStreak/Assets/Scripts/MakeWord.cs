using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MakeWord : MonoBehaviour
{
    public GameObject letter;
    //public AudioSource source;
    public Slider slider;
    public List<GameObject[]> wordList;
    public Animator animator;
    [SerializeField] AudioSource effectsSource, ambientSource;
    [SerializeField] AudioClip celebration, stumble, backspace;
    private char currChar;
    private string[] dictionary;
    private string currPlayer = "Global", wordString = "",statString = "";
    public int difficulty = 28;
    private int childs = 0, wordPos = 0,letterPos = 0, wholePos = 0,lives,mode,ScreenPos;

    private float perCalc = 0.0f, correct = 0.0f, incorrect = 0.0f, wordPer = 0.7f;
    private float volume = 0.5f,effVolume = 0.5f, amVolume = 0.5f, ambientPosition = 0.000f;
    [SerializeField] GameObject hpBar;
    //make KrillStreak asap
    void Start()
    {





        //Set Volumes and Play continue to play ambient track.
        volume = PlayerPrefs.GetFloat("Volume");
        effVolume = volume * PlayerPrefs.GetFloat("EffVolume");
        amVolume = volume * PlayerPrefs.GetFloat("AmVolume");
        ambientPosition = PlayerPrefs.GetFloat("AmbientPosition");
        ambientSource.volume = amVolume;
        ambientSource.Play();
        ambientSource.time = ambientPosition;
        effectsSource.volume = effVolume;

        ScreenPos = 19;
        wordList = new List<GameObject[]>();
        dictionary = MakeDictionary();

        //Gets necessary saved data from playerprefs
        lives = PlayerPrefs.GetInt("Lives");
        int numWords = PlayerPrefs.GetInt("WordCount");
        difficulty = PlayerPrefs.GetInt("Difficulty");
        currPlayer = PlayerPrefs.GetString("currPlayer");
        mode = PlayerPrefs.GetInt("RunDefault@@");

        //Error nullifiction
        if (PlayerPrefs.GetString("currPlayer", "NoPlayerSet") == "NoPlayerSet") 
        {
            currPlayer = "Global";
        }
        //Writes words to UI
        for(int i = 0; i <= numWords; i++)
        {
            writeWord(PickWord());
        }
        //Colours HP Bar
        for (int i = 0; i < lives; i++)
        {
            hpBar.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.green;
        }

    }
    
    void Update()
    {
        if( mode == 1)
        {
            DefaultMode();
        }
        else 
        {
            NonLatinMode();
        }
        getPos();
     }

    void getPos() 
    {
        float pos = 0;
        pos = (float)wholePos / (float)transform.childCount;
        slider.value = pos * 160;
    }
    void writeWord(string word) 
    {
        char[] CharWord = new char[word.Length];
        CharWord = word.ToCharArray();
        GameObject[] newWord = new GameObject[CharWord.Length];

        for(int i = 0; i < CharWord.Length; i++) 
        {
            if(i == 0) 
            {
                GameObject newLetter = GameObject.Instantiate(letter,transform);
                newLetter.GetComponentInChildren<Text>().text = char.ToUpper(CharWord[i]).ToString();
                newWord[i] = newLetter;
            }
            else 
            {
                GameObject newLetter = GameObject.Instantiate(letter, transform);
                newLetter.transform.position = new Vector3(transform.position.x + (Screen.currentResolution.width /ScreenPos) * i, (transform.position.y), (transform.position.z));
                newLetter.GetComponentInChildren<Text>().text = CharWord[i].ToString();
                newWord[i] = newLetter;
            }

        }
        wordList.Add(newWord);
        for(int c = 0; c < wordList[wordList.Count-1].Length; c++)
        {
            
            wordList[wordList.Count-1][c].transform.position = wordList[wordList.Count-1][c].transform.position + new Vector3(0, (Screen.currentResolution.height / -9f) * wordList.Count, 0);
        }
        
    }
    string PickWord() 
    {
        
        string word = dictionary[Random.Range(0, dictionary.Length -1)];

        if (word.Length <= difficulty / 2) { word = PickWord(); }
        //if (difficulty > 28) { return word; }
        else if (word.Length >= difficulty) {  word = PickWord(); }
        
        

        return word;
    }
    string[] MakeDictionary() 
    {
        TextAsset txtFile = (TextAsset)Resources.Load(PlayerPrefs.GetString("SelectedDictionary"), typeof(TextAsset));

        if (txtFile.text.Length != 0)
        {
            string[] fileContent = txtFile.text.Split('\n');
            return fileContent;
        }
        return null;
    }
    void DefaultMode() 
    {

        if (Input.GetKey(KeyCode.Escape)) { PlayerPrefs.SetFloat("AmbientPosition", 0.000f); SceneManager.LoadScene("Menu"); }

        else if (Input.GetKeyDown(KeyCode.Backspace) == true) //handles backspace
        {
            if (letterPos > 0)
            {
                wholePos--; letterPos--; wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.white;
            }
            else { wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.white; }
            wordString = wordString.Substring(0, wordString.Length - 1);
            statString += "-";
            effectsSource.PlayOneShot(backspace,volume);
        }

        else if (Input.anyKeyDown) //handles any other key
        {
            if (Input.inputString[0].ToString() == wordList[wordPos][letterPos].GetComponentInChildren<Text>().text)
            {
                wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.green;
                currChar = wordList[wordPos][letterPos].GetComponentInChildren<Text>().text.ToCharArray()[0];
                wordString += char.ToUpper(currChar);
                statString += char.ToUpper(currChar);
                letterPos++;
                wholePos++;
            }

            else if (Input.inputString[0].ToString() == KeyCode.LeftShift.ToString() || Input.inputString[0].ToString() == KeyCode.RightShift.ToString()) { }
            else if (Input.inputString[0].ToString() == KeyCode.CapsLock.ToString()) { }
            else
            {
                wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.red;
                currChar = wordList[wordPos][letterPos].GetComponentInChildren<Text>().text.ToCharArray()[0];
                wordString += char.ToLower(currChar);
                statString += char.ToLower(currChar);
                letterPos++;
                wholePos++;

            }
        }
        if (letterPos == wordList[wordPos].Length) //checks for word end
        {
            PlayerPrefs.SetString(currPlayer, PlayerPrefs.GetString(currPlayer) + statString);
            PlayerPrefs.SetString("Global", PlayerPrefs.GetString("Global") + statString);
            correct = 0.0f;
            for (int i = 0; i < wordString.Length; i++)
            {
                if (char.IsUpper(wordString[i]) == true) correct+= 1.0f;
            }

            wordPer = correct / (float)wordString.Length;

            wordString = "";
            statString = "";


            //maxdif = 22
            perCalc = 8.5f * ((float)difficulty / 100);
            if (wordPer <= (float)4 * ((float)difficulty / 100) && wordPer != 1.00f)
            {
                hpBar.transform.GetChild(lives - 1).gameObject.GetComponent<Image>().color = Color.red;
                lives--;
                PlayerPrefs.SetInt("Lives", lives);
                if (lives == 0) { PlayerPrefs.SetString("OverText", "Oh No! Your Ran Out Of Lives...\nPick Your Name To See Your Stats!"); PlayerPrefs.SetFloat("AmbientPosition", 0.000f); SceneManager.LoadScene("StatsPage"); }
                //Grabs the animator component and checks that it isn't already playing the celebrate or stumble animation (prevents character flying)
                if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Running@Backflip" || animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Gangnam Style@Floating") { }
                else { animator.SetTrigger("Stumble"); }
            }
            else 
            {
                if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Running@Backflip" || animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Gangnam Style@Floating") { }
                else { animator.SetTrigger("Celebrate"); }
            }
            

            if (wordPos == wordList.Count - 1 && lives != 0) 
            {
                if (difficulty <= 27)
                { 
                    PlayerPrefs.SetInt("Difficulty", PlayerPrefs.GetInt("Difficulty") + 1); 
                }
                PlayerPrefs.SetFloat("AmbientPosition", ambientSource.time);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
            }
            
            correct = 0.0f;
            incorrect = 0.0f;
            letterPos = 0;
            wordPos++;
            wordPer = 0.00f;
            //source.Play();
        }
    }

    void NonLatinMode()
    {
        if (Input.GetKey(KeyCode.Escape)) PlayerPrefs.SetFloat("AmbientPosition", 0.000f); SceneManager.LoadScene("Menu");
        if (Input.GetKeyDown(KeyCode.Backspace) == true) //handles backspace
        {
            if (letterPos > 0)
            {
                wholePos--; letterPos--; wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.white;
            }
            else { wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.white; }
            wordString = wordString.Substring(0, wordString.Length - 1);
        }



        else if (Input.anyKeyDown) //handles any other key
        {
            if (Input.inputString[0].ToString() == wordList[wordPos][letterPos].GetComponentInChildren<Text>().text)
            {
                wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.green;
                currChar = wordList[wordPos][letterPos].GetComponentInChildren<Text>().text.ToCharArray()[0];
                wordString += char.ToUpper(currChar);
                letterPos++;
                wholePos++;

            }

            else if (Input.inputString[0].ToString() == KeyCode.LeftShift.ToString() || Input.inputString[0].ToString() == KeyCode.RightShift.ToString()) { }
            else if (Input.inputString[0].ToString() == KeyCode.CapsLock.ToString()) { }
            else
            {
                wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.red;
                currChar = wordList[wordPos][letterPos].GetComponentInChildren<Text>().text.ToCharArray()[0];
                wordString += char.ToLower(currChar);
                letterPos++;
                wholePos++;


            }
        }
        if (letterPos == wordList[wordPos].Length) //checks for word end
        {
            //PlayerPrefs.SetString(currPlayer, PlayerPrefs.GetString(currPlayer) + wordString);
            //PlayerPrefs.SetString("Global", PlayerPrefs.GetString("Global") + wordString);
            correct = 0.0f;
            for (int i = 0; i < wordString.Length; i++)
            {
                if (char.IsUpper(wordString[i]) == true) correct += 1.0f;
            }

            wordPer = correct / (float)wordString.Length;

            wordString = "";


            //maxdif = 22
            perCalc = 4.5f * ((float)difficulty / 100);
            if (wordPer <= (float)4 * ((float)difficulty / 100) && wordPer != 1.00f)
            {
                hpBar.transform.GetChild(lives - 1).gameObject.GetComponent<Image>().color = Color.red;
                lives--;
                PlayerPrefs.SetInt("Lives", lives);
                if (lives == 0) { PlayerPrefs.SetFloat("AmbientPosition", 0.000f); PlayerPrefs.SetFloat("AmbientPosition", 0.000f); SceneManager.LoadScene("StatsPage"); }
                animator.SetTrigger("Stumble");
            }
            else { animator.SetTrigger("Celebrate"); }


            if (wordPos == wordList.Count - 1 && lives != 0) 
            { 
                if(difficulty <= 27) { PlayerPrefs.SetInt("Difficulty", PlayerPrefs.GetInt("Difficulty") + 1); }
                PlayerPrefs.SetFloat("AmbientPosition", ambientSource.time); 
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

            correct = 0.0f;
            incorrect = 0.0f;
            letterPos = 0;
            wordPos++;
            wordPer = 0.00f;
        }
    }
    void findBiggest()
    {
        string word = "aaaaaaaaaaaaa";
        for(int i = 0; i < dictionary.Length; i++) 
        {
            if (dictionary[i].Length >= word.Length) word = dictionary[i];
        }      
           
    }
    

        
   

}
