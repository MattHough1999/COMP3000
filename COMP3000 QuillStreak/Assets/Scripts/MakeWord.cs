using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public class MakeWord : MonoBehaviour
{
    public GameObject letter;
    public GameObject join;
    private string[] dictionary;
    int childs = 0;
    // Start is called before the first frame update
    void Start()
    {
        dictionary = MakeDictionary();
        writeWord(PickWord());

    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.childCount != childs) 
        {
            transform.position = new Vector3(transform.position.x - (50 * (transform.childCount / 2)), transform.position.y, transform.position.z);
        }
        childs = transform.childCount;
    }
    void writeWord(string word) 
    {
        char[] CharWord = new char[word.Length];
        CharWord = word.ToCharArray();
        
        for(int i = 0; i < CharWord.Length; i++) 
        {
            if(i == 0) 
            {
                
                
                GameObject newLetter = GameObject.Instantiate(letter,transform);
                newLetter.GetComponentInChildren<Text>().text = char.ToUpper(CharWord[i]).ToString();
            }
            else 
            {
                GameObject newJoin = GameObject.Instantiate(join,transform);
                GameObject newLetter = GameObject.Instantiate(letter,transform);
                newLetter.transform.position = new Vector3(transform.position.x + 100 * i, transform.position.y, transform.position.z);
                newJoin.transform.position = new Vector3(transform.position.x + 150 * i, transform.position.y, transform.position.z);
                newLetter.GetComponentInChildren<Text>().text = CharWord[i].ToString();
            }
        }
        
    }
    string PickWord() 
    {
        string word = dictionary[Random.Range(0, dictionary.Length -1)];
        if(word.Length >= 6) { PickWord(); }
        Debug.Log(word);
        return word;
    }
    string[] MakeDictionary() 
    {
        //string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "txt");
        string path = "Assets/Dictionary.txt";
        Debug.Log(path);
        if (path.Length != 0)
        {
            string[] fileContent = File.ReadAllLines(path);
            Debug.Log(fileContent.Length);
            return fileContent;
        }
        return null;
        

    }
}
