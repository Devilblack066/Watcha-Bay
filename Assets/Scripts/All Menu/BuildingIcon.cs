using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIcon : MonoBehaviour
{
    public GameObject Prefab;
    public ConstructionWindow TheWindow;
    public float Price;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        TheWindow.UnselectBuilding();

        TheWindow.SelectABuilding(this.gameObject);
    }
}
