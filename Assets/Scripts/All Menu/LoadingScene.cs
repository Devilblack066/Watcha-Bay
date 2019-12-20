using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingPrefab;
    public GameObject mainMenu;
    public float timeToWait = 0.01f;

    private Image _backgroundImage;
    private Text _text;
    private float _currentView;
    private GameObject _currentLoadingView;
    private LoadingType _type;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        MainMenu.Start_game = this;
    }

    public void StartShowLoadingView(LoadingType type)
    {
        _type = type;
        StartCoroutine("ShowLoadingView");
    }

    private IEnumerator ShowLoadingView()
    {
        mainMenu.SetActive(false);

        InstatiateLoadingView();

        _backgroundImage.color = new Color(0, 0, 0, 0);
        _text.color = new Color(1, 1, 1, 0);
        while (_currentView  < 1)
        {
            yield return new WaitForSeconds(timeToWait);
            _currentView += 0.05f;

            _backgroundImage.color = new Color(0, 0, 0, _currentView);
            _text.color = new Color(255, 201, 0, _currentView);
        }
        DoAction();
    }

    private IEnumerator HideLoadingView()
    {
        while (_currentView > 0)
        {
            yield return new WaitForSeconds(timeToWait);
            _currentView -= 0.05f;

            _backgroundImage.color = new Color(0, 0, 0, _currentView);
            _text.color = new Color(255, 201, 0, _currentView);
        }
        Destroy(gameObject);
    }

    private void InstatiateLoadingView()
    {
        _currentLoadingView = Instantiate(loadingPrefab) as GameObject;

        if (_currentLoadingView == null) return;

        _backgroundImage = _currentLoadingView.transform.Find("backgroundLoad").GetComponent<Image>();
        _text = _currentLoadingView.transform.Find("textLoad").GetComponent<Text>();

        DontDestroyOnLoad(_currentLoadingView);
    }

    private void DoAction()
    {
        switch (_type)
        {
            case LoadingType.Game:
                SceneManager.LoadScene("SampleScene");
                break;
            case LoadingType.MainMenu:
                SceneManager.LoadScene("Main");
                break;
        }

            StartCoroutine("HideLoadingView");
    }
}
