using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadingScene : MonoBehaviour
{
   public void LoadGame()
    {
        MainMenu.Start_game.StartShowLoadingView(LoadingType.Game);
    }

    public void LoadMenu()
    {
        MainMenu.Start_game.StartShowLoadingView(LoadingType.MainMenu);
    }
}
