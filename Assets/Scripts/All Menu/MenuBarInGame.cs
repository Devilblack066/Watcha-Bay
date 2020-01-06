using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarInGame : MonoBehaviour
{
    public GameObject ButtonConstruction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchWithConstructionMode()
    {
        CameraScript.inConstructionMode = !CameraScript.inConstructionMode;
        if (CameraScript.inConstructionMode)
        {
            ButtonConstruction.GetComponent<Image>().color = Color.green;
        }
        else
        {
            ButtonConstruction.GetComponent<Image>().color = Color.white;
        }
    }
}
