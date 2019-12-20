using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingPrefab;

    private Image _backgroundImage;
    private Text _text;
    
    private void Awake()
    {
        MainMenu.Start_game = this;
    }

    private IEnumerator ShowLoadingView()
    {
        _backgroundImage.color = new Color(0, 0, 0, 0);
        _text.color = new Color(255, 255, 255, 0);
    }
}
