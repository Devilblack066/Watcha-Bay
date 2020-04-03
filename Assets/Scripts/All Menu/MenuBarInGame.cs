using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarInGame : MonoBehaviour
{
    public GameObject windowConstruction;
    public GameObject ButtonConstruction;
    public GameObject ButtonDestruction;
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
            Debug.Log("true");
            windowConstruction.SetActive(true);
        }
        else
        {
            ButtonConstruction.GetComponent<Image>().color = Color.white;
            Debug.Log("false");
            windowConstruction.SetActive(false);
        }
    }
    public void SwitchWithDestructionMode()
    {
        CameraScript.inDestructionMode = !CameraScript.inDestructionMode;
        if (CameraScript.inDestructionMode && CameraScript.inConstructionMode == false)
        {
            ButtonDestruction.GetComponent<Image>().color = Color.red;
            Debug.Log("true");
        }
        else
        {
            ButtonDestruction.GetComponent<Image>().color = Color.white;
            Debug.Log("false or in ConstructionMode");
        }
    }
}
