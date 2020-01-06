using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ObjectPositioner : MonoBehaviour
{
    private GridScript grid;
    public GameObject actualbat;
    public GameObject objetinst = null;


    public int xtoint;
    public int ytoint;


    public GridScript gd ;

    Ray ray;
    RaycastHit hitInfo;

[SerializeField]
    public GameObject batiments;

    //[SerializeField]
    //public Camera cam;

    private void Awake()
    {
       // gd = FindObjectOfType<GridScript>();
        //WriteString();
    }

    public void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                    PlaceCubeNear(hitInfo.point,batiments);
            }
        }*/
    }
 
    public void PlaceCubeNear(Vector3 clickPoint,GameObject batiment)
    {



        var finalPosition = gd.GetNearestPointOnGrid(clickPoint);
                xtoint = gd.GridTabVal.GetLength(1) - (int) finalPosition.z-1;
                ytoint = (int) finalPosition.x;

        //Debug.Log(xtoint);
       //Debug.Log(ytoint);
        if (TestPossible(batiment, xtoint, ytoint) == false ) return;
        else
        {
            
            objetinst = Instantiate(batiment, clickPoint, Quaternion.identity);
            objetinst.transform.position = finalPosition;
            SpawnBatGridTab(objetinst, xtoint, ytoint);
            ShowMatrice0And1();
            actualbat = objetinst;
        }
    }


    public void SpawnBatGridTab(GameObject go,int xtoint , int ytoint)
    {
        var objY= (int)Mathf.Round(go.transform.localScale.x/2);
        var objX= (int)Mathf.Round(go.transform.localScale.z/2);

        //Debug.Log((int)Mathf.Round(xtoint+objX));
        //Debug.Log((int)Mathf.Round(ytoint+objY));

        /*var Pointertogrid = gd.GridTabPos[xtoint,ytoint];

        gd.GridTabVal[xtoint,ytoint] = 1.0f;
        if(objX != 1.0f && objY != 1.0f){
            for(var i=1; i<objX;i++){
                gd.GridTabVal[xtoint+i,0] = 1.0f;
                gd.GridTabVal[xtoint-i,0] = 1.0f;
                Debug.Log( gd.GridTabVal[xtoint+i,0] + " - "+" - "+ gd.GridTabVal[xtoint-i,0]);
                    for(var z=0; z<= objY;z++){
                        gd.GridTabVal[i,ytoint+z] = 1.0f;
                        gd.GridTabVal[i,ytoint-z] = 1.0f;
                    }
            }
            
        }*/
        for (int i = 0; i<=objX;++i)
        {
            for (int j = 0; j<=objY;++j)
            {
                gd.GridTabVal[xtoint + i, ytoint + j] = 1.0f;
                gd.GridTabVal[xtoint - i, ytoint + j] = 1.0f;
                gd.GridTabVal[xtoint + i, ytoint - j] = 1.0f;
                gd.GridTabVal[xtoint - i, ytoint - j] = 1.0f;
            }
        }

    }

    public bool TestPossible(GameObject go, int xtoint, int ytoint)
    {
        var objY = (int)Mathf.Round(go.transform.localScale.x / 2);
        var objX = (int)Mathf.Round(go.transform.localScale.z / 2);

        //Debug.Log(objX); Debug.Log(objY);
        //Debug.Log(xtoint); Debug.Log(ytoint);
        //Debug.Log((int)Mathf.Round(xtoint+objX));
        //Debug.Log((int)Mathf.Round(ytoint+objY));

        /*var Pointertogrid = gd.GridTabPos[xtoint,ytoint];

        gd.GridTabVal[xtoint,ytoint] = 1.0f;
        if(objX != 1.0f && objY != 1.0f){
            for(var i=1; i<objX;i++){
                gd.GridTabVal[xtoint+i,0] = 1.0f;
                gd.GridTabVal[xtoint-i,0] = 1.0f;
                Debug.Log( gd.GridTabVal[xtoint+i,0] + " - "+" - "+ gd.GridTabVal[xtoint-i,0]);
                    for(var z=0; z<= objY;z++){
                        gd.GridTabVal[i,ytoint+z] = 1.0f;
                        gd.GridTabVal[i,ytoint-z] = 1.0f;
                    }
            }  
        }*/

        if (gd.GridTabVal.GetLength(0) <= objX + xtoint || gd.GridTabVal.GetLength(1) <= objY + ytoint || xtoint - objX < 0 || ytoint - objY < 0)
        {
            Debug.Log("ça a pété");
            Debug.Log(gd.GridTabVal.GetLength(0));
            Debug.Log(gd.GridTabVal.GetLength(1));
            return false;
        }

        for (int i = 0; i <= objX; ++i)
        {
            for (int j = 0; j <= objY; ++j)
            {
                /*gd.GridTabVal[xtoint + i, ytoint + j] = 1.0f;
                gd.GridTabVal[xtoint - i, ytoint + j] = 1.0f;
                gd.GridTabVal[xtoint + i, ytoint - j] = 1.0f;
                gd.GridTabVal[xtoint - i, ytoint - j] = 1.0f;*/
                if (gd.GridTabVal[xtoint + i, ytoint + j] == 1.0f)
                {
                    Debug.Log("ça a pété 1 i-> " +i +", j ->"+ j);
                    return false;
                }
                if (gd.GridTabVal[xtoint - i, ytoint + j] == 1.0f)
                {
                    Debug.Log("ça a pété 2 i-> " + i + ", j ->" + j);
                    return false;
                }
                if (gd.GridTabVal[xtoint + i, ytoint - j] == 1.0f)
                {
                    Debug.Log("ça a pété 3 i-> " + i + ", j ->" + j);
                    return false;
                }
                if (gd.GridTabVal[xtoint - i, ytoint - j] == 1.0f)
                {
                    Debug.Log("ça a pété 4 i-> " + i + ", j ->" + j);
                    return false;
                }
            }
        }
        return true;
    }
    void ShowMatrice0And1()
    {
        //string path = "Assets/Resources/test.txt";

        /*File.Delete(path);
        File.Create(path);*/

        //Write some text to the test.txt file
        //StreamWriter writer = new StreamWriter(path, true);
        for (int i = 0; i<gd.GridTabVal.GetLength(0);++i)
        {
            string chaine = "";
            for (int y = 0; y < gd.GridTabVal.GetLength(1); ++y)
            {
                chaine += gd.GridTabVal[i,y].ToString();
            }
            Debug.Log(chaine);
        }
        //writer.WriteLine("\n\n\n");
        //writer.Close();

        //Re-import the file to update the reference in the editor
        /*AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load("test") as TextAsset;*/

        //Print the text from the file
        //Debug.Log(asset.text);
    }
}