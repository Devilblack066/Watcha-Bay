using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANeed : MonoBehaviour
{
    public typeOfNeed Type;
    public string Name;
    public int Value;
    public int MaxValue;

    float timer = 0.0f;


    ANeed(typeOfNeed ton, string s, int val, int max) { Type = ton; Name = s; Value = val;  MaxValue = max; }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public static ANeed[] generateBasicalNeeds()
    {
        return new ANeed[] {new ANeed(typeOfNeed.Happinness,"Happiness",100,100),
                            new ANeed(typeOfNeed.Hunger,"Hunger",100,100),
                            new ANeed(typeOfNeed.Thirst,"Thrist",100,100),
                            new ANeed(typeOfNeed.Entertainment,"Entertainment",100,100),
                            new ANeed(typeOfNeed.Tiredness,"Tiredness",100,100),
                            new ANeed(typeOfNeed.Hygiene,"Hygiene",100,100)};
    }
}

public enum typeOfNeed
{
    Happinness,
    Hunger,
    Thirst,
    Entertainment,
    Tiredness,
    Hygiene
}