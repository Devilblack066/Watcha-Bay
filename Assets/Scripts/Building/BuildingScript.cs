using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject Entrance;
    public string BuildName;
    public Dictionary<string, BonusCorrespondance> theBonus;
    public int Price; 
    // Start is called before the first frame update

    public void CatchBuildingBonus()
    {
        theBonus = new Dictionary<string, BonusCorrespondance>();
        BuildingFromFile build = generalFunctions.FindStatsBuildingFromName(BuildName);
        //theBonus = build.listOfBonusMultiplier;
        foreach (KeyValuePair<string,BonusCorrespondance> keyvalue in build.listOfBonusMultiplier)
        {
            theBonus.Add(keyvalue.Key, keyvalue.Value);
        }
        Debug.Log(theBonus.Count);
        Price = build.price;
    }

    public void ShowBonus()
    {
        foreach (KeyValuePair<string,BonusCorrespondance> corresp in theBonus)
        {
            Debug.Log(corresp.Key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
