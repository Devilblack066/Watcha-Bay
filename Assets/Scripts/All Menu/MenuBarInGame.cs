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

    void setActiveGrid(float act)
    {
        for (float x = 0; x < gs.gridWorldSizeX / gs.size; x++)
        {
            for (float z = 0; z < gs.gridWorldSizeY / gs.size; z++)
            {
                if(gs.GridTabVal[(int)x, (int)z] == act)gs.GridTabGo[(int)x, (int)z].SetActive(true);
                //Debug.Log(GridTabGo[x1,z1]);

            }
        }
    }
    void unShowGrid()
    {
        for (float x = 0; x < gs.gridWorldSizeX / gs.size; x++)
        {
            for (float z = 0; z < gs.gridWorldSizeY / gs.size; z++)
            {
                gs.GridTabGo[(int)x, (int)z].SetActive(false);
                //Debug.Log(GridTabGo[x1,z1]);
            }
        }
    }

    public void SwitchMode(CameraScript.StateEnum state)
    {
        ClearButtons();
        windowConstruction.SetActive(false);
        unShowGrid();
        if (state == cam.Typestate) state = CameraScript.StateEnum.Normal;
        cam.setEnumState(state);
        switch (state)
        {
            case CameraScript.StateEnum.Construction:
                ButtonConstruction.GetComponent<Image>().color = Color.green;
                windowConstruction.SetActive(true);
                setActiveGrid(0);
                break;
            case CameraScript.StateEnum.Destruction:
                ButtonDestruction.GetComponent<Image>().color = Color.red;
                setActiveGrid(1);
                break;
            case CameraScript.StateEnum.Normal:
                break;
        }
    }

    public void ClearButtons()
    {
        ButtonConstruction.GetComponent<Image>().color = Color.white;
        ButtonDestruction.GetComponent<Image>().color = Color.white;
    }

    public void SwitchWithConstructionMode()
    {
        SwitchMode(CameraScript.StateEnum.Construction);
    }
    public void SwitchWithDestructionMode()
    {
        SwitchMode(CameraScript.StateEnum.Destruction);
    }
}
