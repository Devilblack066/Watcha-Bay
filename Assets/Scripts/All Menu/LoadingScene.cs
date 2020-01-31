using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingPrefab;
    public GameObject mainMenu;
    public float timeToWait = 0.05f;

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
        Debug.Log("StartShowLoadingView");
        _type = type;
        StartCoroutine("ShowLoadingView");
    }

    private IEnumerator ShowLoadingView()
    {
        Debug.Log("ShowLoadingView");
        InstatiateLoadingView();
        

        _backgroundImage.color = new Color(0, 0, 0, 0);
        _text.color = new Color(1, 1, 1, 0);
        Debug.Log("_currentView: " + _currentView);
        _currentView = 0;
        while (_currentView  < 1)
        {
            //yield return new WaitForSeconds(timeToWait);
            Debug.Log("after wait for second");
            _currentView += 0.05f;
            Debug.Log(_currentView);

            _backgroundImage.color = new Color(0, 0, 255, _currentView);
            _text.color = new Color(255, 201, 0, _currentView);
            yield return 1;
        }
        DoAction();
    }

    private IEnumerator HideLoadingView()
    {
        while (_currentView > 0)
        {
            //yield return new WaitForSeconds(timeToWait);
            _currentView -= 0.05f;
            Debug.Log("currentView:" + _currentView);

            _backgroundImage.color = new Color(0, 0, 255, _currentView);
            _text.color = new Color(255, 201, 0, _currentView);
            yield return 1;
        }
        Destroy(_currentLoadingView);
    }

    private void InstatiateLoadingView()
    {
        Debug.Log("InstatiateLoadingView");
        _currentLoadingView = Instantiate(loadingPrefab) as GameObject;

        if (_currentLoadingView == null) return;

        _backgroundImage = _currentLoadingView.transform.Find("backgroundLoad").GetComponent<Image>();
        _text = _currentLoadingView.transform.Find("textLoad").GetComponent<Text>();

        DontDestroyOnLoad(_currentLoadingView);
    }

    private void DoAction()
    {
        Debug.Log("DoAction");
        switch (_type)
        {
            case LoadingType.Game:
                mainMenu.SetActive(false);
                SceneManager.LoadScene("SampleScene");
                break;
            case LoadingType.MainMenu:
                SceneManager.LoadScene("Julian");
                break;
        }

            StartCoroutine("HideLoadingView");
    }
}
