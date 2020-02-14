﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class generalFunctions : MonoBehaviour
{
    static public List<string> allLastNames;
    static public List<string> allFirstNames;

    static public Color GreenColor = new Color(0, 170, 0);
    static public Color RedColor = new Color(170, 0, 0);

    static public List<BonusCorrespondance> allBonus;
    static public List<BuildingFromFile> allBuildings;
    // Start is called before the first frame update
    void Start()
    {
        InitNames();
        //Debug.Log(Application.persistentDataPath+ "/StreamingAssets/");
        InitAllBonus();
        InitAllStatsBuilding();
        showBuilding();
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


    static void InitAllBonus()
    {
        allBonus = new List<BonusCorrespondance>();
        allBonus.Add(new BonusCorrespondance(BonusMultiplier.X3, "+++", GreenColor));
        allBonus.Add(new BonusCorrespondance(BonusMultiplier.X2, "++", GreenColor));
        allBonus.Add(new BonusCorrespondance(BonusMultiplier.X1, "+", GreenColor));
        allBonus.Add(new BonusCorrespondance(BonusMultiplier.M3, "---", RedColor));
        allBonus.Add(new BonusCorrespondance(BonusMultiplier.M2, "--", RedColor));
        allBonus.Add(new BonusCorrespondance(BonusMultiplier.M1, "-", RedColor));
    }

    static BonusCorrespondance ReturnTheBonus(BonusMultiplier bonus)
    {
        foreach (BonusCorrespondance b in allBonus)
        {
            if (b.EnumMultiplier == bonus) return b;
        }
        return null;
    }

    static BonusCorrespondance ReturnTheBonus(string bonus)
    {
        foreach (BonusCorrespondance b in allBonus)
        {
            if (b.TextBonus == bonus) return b;
        }
        return null;
    }

    static void InitAllStatsBuilding()
    {
        //allLastNames = new List<string>();
        allBuildings = new List<BuildingFromFile>();


        TextAsset file = Resources.Load("listOfBuildings") as TextAsset;
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
        List<string> entete = new List<string>();
        string[] lines = fileData.Split('\n');
        for (int i = 0; i < lines.Length; ++i)
        {
            string[] lineData = lines[i].Split(';');
            if (i == 0) {
                for (int j = 0; j < lineData.Length; ++j)
                {
                    entete.Add(lineData[j]);  
                }
            }
            else
            {

               // Debug.Log(lineData[(lineData.Length - 2)]);
                BuildingFromFile building = new BuildingFromFile(lineData[0], int.Parse(lineData[(lineData.Length-2)]));
                for (int j = 0; j < lineData.Length-2; ++j)
                {
                    if (j != 0 && lineData[j]!="")
                    {
                        //Debug.Log( j+" "+entete[j]+" "+lineData[j]);
                        building.listOfBonusMultiplier.Add(entete[j],ReturnTheBonus(lineData[j]));
                    }
                }
                allBuildings.Add(building);
            }
        }
    }

    static public void showBuilding()
    {
        foreach (BuildingFromFile bu in allBuildings)
        {
            //Debug.Log(bu.name);
            foreach(KeyValuePair<string,BonusCorrespondance> dico in bu.listOfBonusMultiplier)
            {
                //Debug.Log(dico.Key + " " + dico.Value.TextBonus);
            }
            //Debug.Log("Price ->" + bu.price);
        }
    }

    static public BuildingFromFile FindStatsBuildingFromName(string n)
    {
        foreach (BuildingFromFile b in allBuildings)
        {
            if(n == b.name)
            {
                return b;
            }
        }
        return null;
    }
}

public class BuildingFromFile
{
    public BuildingFromFile(string n, int p)
    {
        name = n;
        price = p;
        listOfBonusMultiplier = new Dictionary<string, BonusCorrespondance>();
    }
    public string name;
    public Dictionary<string, BonusCorrespondance> listOfBonusMultiplier;
    public int price;
}

