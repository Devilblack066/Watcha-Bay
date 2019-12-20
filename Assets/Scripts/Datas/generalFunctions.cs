using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class generalFunctions : MonoBehaviour
{
    static public List<string> allLastNames;
    static public List<string> allFirstNames;
    // Start is called before the first frame update
    void Start()
    {
        InitNames();
        //Debug.Log(Application.persistentDataPath+ "/StreamingAssets/");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void InitNames()
    {
        allLastNames = new List<string>();
        allFirstNames = new List<string>();


        TextAsset file = Resources.Load("listOfNames") as TextAsset;
        string fileData = file.text;
        //string pathCSVNames = Application.dataPath + "/StreamingAssets/listOfNames.csv";




        /*if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            WWW reader = new WWW(pathCSVNames);
            while (!reader.isDone) { }
            fileData = reader.text;
        }
        else
        {
            fileData = System.IO.File.ReadAllText(Application.persistentDataPath + "/listOfNames.csv");
        }*/

        string[] lines = fileData.Split('\n');
        for (int i = 0 ; i < lines.Length ; ++i)
        {
            string[] lineData = lines[i].Split(';');
            if(lineData[0] != "")allFirstNames.Add(lineData[0]);
            if(lineData[1] != "")allLastNames.Add(lineData[1]);
        }
    }
}
