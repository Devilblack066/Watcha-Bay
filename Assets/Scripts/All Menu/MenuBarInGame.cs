using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarInGame : MonoBehaviour
{
    public GameObject windowConstruction;
    public GameObject ButtonConstruction;
    public GameObject ButtonDestruction;

    public GameObject[,] gridtab;

    CameraScript cam;
    GridScript gs;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("TheCamera").GetComponentInChildren<CameraScript>();
        gs = GameObject.Find("Grid").GetComponentInChildren<GridScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setActiveGrid(bool act)
    {
        for (float x = 0; x < gs.gridWorldSizeX / gs.size; x++)
        {
            for (float z = 0; z < gs.gridWorldSizeY / gs.size; z++)
            {
                gs.GridTabGo[(int)x, (int)z].SetActive(act);
                //Debug.Log(GridTabGo[x1,z1]);

            }
        }
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
                setActiveGrid(true);
            }
            else
            {
                cam.setEnumState(CameraScript.StateEnum.Normal);
                ButtonConstruction.GetComponent<Image>().color = Color.white;
                Debug.Log("false");
                windowConstruction.SetActive(false);
                setActiveGrid(false);
            }
        }
        else
        {
            cam.setEnumState(CameraScript.StateEnum.Normal);
            ButtonConstruction.GetComponent<Image>().color = Color.white;
            ButtonDestruction.GetComponent<Image>().color = Color.white;
            Debug.Log("false");
            windowConstruction.SetActive(false);
            setActiveGrid(false);
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
                setActiveGrid(false);
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
