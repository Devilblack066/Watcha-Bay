using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Option ()
    {
        SceneManager.LoadScene(1);
        SceneManager.LoadScene(2);
    }

    public void Quitter ()
    {
        Debug.Log("Application Fermée");
        Application.Quit();
    }

    public static LoadingScene Start_game;
}

public enum LoadingType
{
    None, Game, MainMenu
}
