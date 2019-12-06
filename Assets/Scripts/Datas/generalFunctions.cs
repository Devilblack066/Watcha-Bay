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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void InitNames()
    {
        allLastNames = new List<string>();
        allFirstNames = new List<string>();
        string fileData = System.IO.File.ReadAllText("./Assets/Scripts/Datas/listOfNames.csv");
        string[] lines = fileData.Split('\n');
        for (int i = 0 ; i < lines.Length ; ++i)
        {
            string[] lineData = lines[i].Split(';');
            if(lineData[0] != "")allFirstNames.Add(lineData[0]);
            if(lineData[1] != "")allLastNames.Add(lineData[1]);
        }
    }
}
