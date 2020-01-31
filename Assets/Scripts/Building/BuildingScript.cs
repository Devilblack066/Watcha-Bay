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
        theBonus = build.listOfBonusMultiplier;
        Price = build.price;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
