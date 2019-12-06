using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class Barre_hud : MonoBehaviour
{
    public BayStats Stats;
    public Text Pop_Max;
    public Text Pop_Actuelle;
    public Text Usr_Money;

    void Update ()
    {
        int[] statsSwimmers = Stats.getSwimmerNumber();
        int statMoney = Stats.getUserMoney();
        Pop_Actuelle.text = statsSwimmers[0].ToString();
        Pop_Max.text = "/ " + statsSwimmers[1].ToString();
        Usr_Money.text = statMoney.ToString() + "£";

    }
}

