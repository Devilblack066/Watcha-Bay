using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionWindow : MonoBehaviour
{
    public GameObject SelectedBuild = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnselectBuilding()
    {
        if (SelectedBuild != null)
        {
            SelectedBuild.GetComponent<Image>().color = Color.white;
        }
    }
    public void SelectABuilding(GameObject go)
    {
        if (go == SelectedBuild)
        {
            SelectedBuild = null;
            go.GetComponent<Image>().color = Color.white;
        }
        else
        {
            SelectedBuild = go;
            SelectedBuild.GetComponent<Image>().color = Color.green;
        }
    }
}
