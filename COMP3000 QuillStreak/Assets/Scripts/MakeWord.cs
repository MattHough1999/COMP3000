using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor;
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
    public int difficulty = 6;
    private string[] dictionary;
    private int childs = 0;
    private int wordPos = 0;
    private int letterPos = 0;
    private int wholePos = 0;
    private float wordPer = 0.7f;
    // private Slider jimmy;
    //make KrillStreak asap
    void Start()
    {
        
        wordList = new List<GameObject[]>();
        dictionary = MakeDictionary();
        int numWords = PlayerPrefs.GetInt("WordCount");
        difficulty = PlayerPrefs.GetInt("Difficulty");
        for(int i = 0; i <= numWords; i++)
        {
            writeWord(PickWord());
        }
        //writeWord(PickWord());
    }
    
    
    void Update()
    {
        
        if(transform.childCount != childs) 
        {
            transform.position = new Vector3(transform.position.x - (50 * (transform.childCount / wordList.Count)), transform.position.y, transform.position.z);
        }
        childs = transform.childCount;
        DefaultMode();
        getPos();
     }

    
    void getPos() 
    {
        float pos = 0;
        pos = (float)wholePos / (float)transform.childCount;
        slider.value = pos * 160;
        Debug.Log(pos.ToString());
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
                newLetter.transform.position = new Vector3(transform.position.x + 130f * i, (transform.position.y), (transform.position.z));
                newLetter.GetComponentInChildren<Text>().text = CharWord[i].ToString();
                newWord[i] = newLetter;
            }

        }
        wordList.Add(newWord);
        for(int c = 0; c < wordList[wordList.Count-1].Length; c++)
        {
            wordList[wordList.Count-1][c].transform.position = wordList[wordList.Count-1][c].transform.position + new Vector3(0, -130f * wordList.Count, 0);
        }
        
    }
    string PickWord() 
    {
        string word = dictionary[Random.Range(0, dictionary.Length -1)];
               
        if(word.Length >= difficulty) { Debug.Log(word); word = PickWord(); }
        else if(word.Length <= difficulty / 2) { Debug.Log(word); word = PickWord(); }

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
        
        if (Input.GetKeyDown(KeyCode.Backspace) == true) //handles backspace
        {
            if (letterPos > 0)
            {
                wholePos--; letterPos--;  wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.white;
            }
            else { wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.white; }
        }

        else if (Input.anyKeyDown) //handles any other key
        {
            if (Input.inputString[0].ToString() == wordList[wordPos][letterPos].GetComponentInChildren<Text>().text)
            {
                wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.green;
                Debug.Log(wordList[wordPos].Length);
                //wordPer += wordList[wordPos].Length/100;
                letterPos++;
                wholePos++;
            }

            else if (Input.inputString[0].ToString() == KeyCode.LeftShift.ToString() || Input.inputString[0].ToString() == KeyCode.RightShift.ToString()) { }
            else if (Input.inputString[0].ToString() == KeyCode.CapsLock.ToString()) { }
            else { wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.red; letterPos++; wholePos++; }
        }
        if (letterPos == wordList[wordPos].Length) //checks for word end
        {
            
            if (wordPos == wordList.Count - 1) { PlayerPrefs.SetInt("Difficulty", PlayerPrefs.GetInt("Difficulty") + 1); SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
            Debug.Log(wordPer);
            if (wordPer <= 0.65f)
            {
                animator.SetTrigger("Stumble");
            }
            else { animator.SetTrigger("Celebrate"); }
            letterPos = 0;
            wordPos++;
            //wordPer = 0.00f;
            //source.Play();
        }
    }
   
}
