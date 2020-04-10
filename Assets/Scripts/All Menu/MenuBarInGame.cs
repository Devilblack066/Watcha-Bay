using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarInGame : MonoBehaviour
{
    public GameObject windowConstruction;
    public GameObject ButtonConstruction;
    public GameObject ButtonDestruction;

    CameraScript cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("TheCamera").GetComponentInChildren<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchWithConstructionMode()
    {
        if (cam.Typestate != CameraScript.StateEnum.Construction)
        {
            cam.setEnumState(CameraScript.StateEnum.Construction);
            if (cam.Typestate == CameraScript.StateEnum.Construction)
            {
                ButtonConstruction.GetComponent<Image>().color = Color.green;
                ButtonDestruction.GetComponent<Image>().color = Color.white;
                Debug.Log("true");
                windowConstruction.SetActive(true);
            }
            else
            {
                cam.setEnumState(CameraScript.StateEnum.Normal);
                ButtonConstruction.GetComponent<Image>().color = Color.white;
                Debug.Log("false");
                windowConstruction.SetActive(false);
            }
        }
        else
        {
            cam.setEnumState(CameraScript.StateEnum.Normal);
            ButtonConstruction.GetComponent<Image>().color = Color.white;
            ButtonDestruction.GetComponent<Image>().color = Color.white;
            Debug.Log("false");
            windowConstruction.SetActive(false);
        }
    }
    public void SwitchWithDestructionMode()
    {
        if (cam.Typestate != CameraScript.StateEnum.Destruction)
        {
            cam.setEnumState(CameraScript.StateEnum.Destruction);
            if (cam.Typestate == CameraScript.StateEnum.Destruction)
            {
                ButtonDestruction.GetComponent<Image>().color = Color.red;

                ButtonConstruction.GetComponent<Image>().color = Color.white;
                Debug.Log("true");

                windowConstruction.SetActive(false);
            }
            else
            {
                cam.setEnumState(CameraScript.StateEnum.Normal);
                ButtonDestruction.GetComponent<Image>().color = Color.white;
                Debug.Log("false or in ConstructionMode");
            }
        }else{
            cam.setEnumState(CameraScript.StateEnum.Normal);
            ButtonConstruction.GetComponent<Image>().color = Color.white;

            ButtonDestruction.GetComponent<Image>().color = Color.white;
            Debug.Log("false or in ConstructionMode");
        }
    }
}
