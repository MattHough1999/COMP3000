using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MakeWord : MonoBehaviour
{
    public GameObject letter;
    public List<GameObject[]> wordList;
    public int difficulty = 6;
    private string[] dictionary;
    private int childs = 0;
    private KeyCode lastKey = KeyCode.A;
    private int wordPos = 0;
    private int letterPos = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        wordList = new List<GameObject[]>();
        dictionary = MakeDictionary();
        int numWords = Random.Range(0, 4);
        for(int i = 0; i < numWords; i++)
        {
            writeWord(PickWord());
        }

        writeWord(PickWord());


    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount != childs) 
        {
            transform.position = new Vector3(transform.position.x - (50 * (transform.childCount / wordList.Count)), transform.position.y, transform.position.z);
        }
        childs = transform.childCount;

        if (Input.GetKeyDown(KeyCode.Backspace) == true)
        {
            if (letterPos > 0)
            {
                letterPos--; wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.white;  
            }
            else { wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.white; }
        }

        else if (Input.anyKeyDown)
        {
            if (Input.inputString[0].ToString() == wordList[wordPos][letterPos].GetComponentInChildren<Text>().text)
            {
                wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.green;
                letterPos++;

                //make KrillStreak asap
            }
            
            else if (Input.inputString[0].ToString() == KeyCode.LeftShift.ToString() || Input.inputString[0].ToString() == KeyCode.RightShift.ToString()) { }
            else if (Input.inputString[0].ToString() == KeyCode.CapsLock.ToString()) { }
            else { wordList[wordPos][letterPos].GetComponentInChildren<Image>().color = Color.red; letterPos++; }
        }
        if (letterPos == wordList[wordPos].Length) 
        {
            if(wordPos == wordList.Count -1) { SceneManager.LoadScene("Space");  }
            letterPos = 0;
            wordPos++;
        }

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
                newLetter.transform.position = new Vector3(transform.position.x + 100 * i, (transform.position.y), (transform.position.z));
                newLetter.GetComponentInChildren<Text>().text = CharWord[i].ToString();
                newWord[i] = newLetter;
            }

        }
        wordList.Add(newWord);
        for(int c = 0; c < wordList[wordList.Count-1].Length; c++)
        {
            wordList[wordList.Count-1][c].transform.position = wordList[wordList.Count-1][c].transform.position + new Vector3(0, -100 * wordList.Count, 0);
        }
        
    }
    string PickWord() 
    {
        string word = dictionary[Random.Range(0, dictionary.Length -1)];
        if(word.Length >= difficulty) { Debug.Log(word); word = PickWord(); }
        else if(word.Length >= difficulty / 2) { Debug.Log(word); word = PickWord(); }
        
        return word;
    }
    string[] MakeDictionary() 
    {
        string path = "Dictionary.txt";
        
        if (path.Length != 0)
        {
            string[] fileContent = File.ReadAllLines(path);
            
            return fileContent;
        }
        return null;
    }
   
}
