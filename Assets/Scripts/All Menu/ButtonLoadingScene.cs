using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadingScene : MonoBehaviour
{
   public void LoadGame()
    {
        Debug.Log("LoadGame");
        MainMenu.Start_game.StartShowLoadingView(LoadingType.Game);
    }

    public void LoadMenu()
    {
        Debug.Log("LoadMenu");
        MainMenu.Start_game.StartShowLoadingView(LoadingType.MainMenu);
    }
}
