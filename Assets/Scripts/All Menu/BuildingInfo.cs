using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoBehaviour
{
    public Text BuildingName;
    public Text TotalPrice;


    public GameObject tupleBesoin;

    public GameObject gridParent;

    public BuildingScript theBuild;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateInfo()
    {
        if (CameraScript.inConstructionMode && GetComponentInParent<ConstructionWindow>().SelectedBuild != null)
        {
            transform.gameObject.SetActive(true);
            theBuild = GetComponentInParent<ConstructionWindow>().SelectedBuild.GetComponent<BuildingIcon>().Prefab.GetComponent<BuildingScript>();
            BuildingName.text = theBuild.BuildName;
            TotalPrice.text = theBuild.Price.ToString() + "£";
            clearTheGrid();
            InstantiateAllTuples();
        }
        else
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void clearTheGrid()
    {
        int childs = gridParent.transform.childCount;
        for (int i = 0; i < childs; ++i)
        {
            GameObject.Destroy(gridParent.transform.GetChild(i).gameObject);
        }
    }
    public void InstantiateAllTuples()
    {
        foreach(KeyValuePair<string,BonusCorrespondance> b in theBuild.theBonus)
        {
            GameObject instance = Instantiate(tupleBesoin, gridParent.transform.position,gridParent.transform.rotation,gridParent.transform);
            instance.GetComponent<BuildingTupleInfo>().NameBonus.text = b.Key;
            instance.GetComponent<BuildingTupleInfo>().BonusValue.text = b.Value.TextBonus;
            instance.GetComponent<BuildingTupleInfo>().BonusValue.color = b.Value.color;
        }
    }
}
