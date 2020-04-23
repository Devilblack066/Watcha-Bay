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
 
    public void PlaceCubeNear(Vector3 clickPoint, GameObject batiment, BayStats theBay, int Price)
    {
        var finalPosition = gd.GetNearestPointOnGrid(clickPoint);
                //xtoint = gd.GridTabVal.GetLength(1) - (int) finalPosition.z-1;
                //ytoint = (int) finalPosition.x;

        //Debug.Log(finalPosition);
       //Debug.Log(ytoint);
        if (TestPossible(batiment, finalPosition.x, finalPosition.z) == false ) return;
        else
        {
            //Debug.Log("tu es passé");
            theBay.PaySomething(Price);
            objetinst = Instantiate(batiment, clickPoint, Quaternion.identity);
            objetinst.transform.position = finalPosition;
            SpawnBatGridTab(objetinst, finalPosition.x, finalPosition.z);
            //ShowMatrice0And1();
            actualbat = objetinst;
        }
    }

    public void SellCubeNear(Vector3 clickPoint,GameObject batiment, BayStats theBay, int Price)
    {
            var finalPosition = gd.GetNearestPointOnGrid(clickPoint);

            //Debug.Log("tu es passé");
            theBay.SellSomething(Mathf.RoundToInt(Price/2));
            Destroy(batiment);
            DespawnBatGridTab(finalPosition.x, finalPosition.z);
    }


    public void SpawnBatGridTab(GameObject go,float xtoint , float ytoint)
    {

        Vector2 vec = gd.GetNearestRefOnGrid(new Vector3(xtoint,0,ytoint));
        //Debug.Log(vec);
        if(vec.x == -99999)return;
        gd.GridTabVal[(int)vec.x,(int)vec.y] = 1.0f;
        Debug.Log(" Valeur de la grille ->"+gd.GridTabVal[(int)vec.x, (int)vec.y] + " Pour i =" +(int)vec.x + " et pour j ="+ (int)vec.y);
        gd.GridTabGo[(int)vec.x,(int)vec.y].GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 0.75f);
        gd.GridTabGo[(int)vec.x, (int)vec.y].SetActive(false);
    }

    public void DespawnBatGridTab(float xtoint, float ytoint)
    {

        Vector2 vec = gd.GetNearestRefOnGrid(new Vector3(xtoint, 0, ytoint));
        //Debug.Log(vec);
        if (vec.x == -99999) return;
        gd.GridTabVal[(int)vec.x, (int)vec.y] = 0.0f;
        Debug.Log(" Valeur de la grille ->" + gd.GridTabVal[(int)vec.x, (int)vec.y] + " Pour i =" + (int)vec.x + " et pour j =" + (int)vec.y);
        gd.GridTabGo[(int)vec.x,(int)vec.y].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.75f);
        gd.GridTabGo[(int)vec.x, (int)vec.y].SetActive(false);
    }


    public bool TestPossible(GameObject go, float xtoint, float ytoint)
    {
        //var objY = (int)Mathf.Round(go.transform.localScale.x / 2);
        //var objX = (int)Mathf.Round(go.transform.localScale.z / 2);

        //---Debug.Log(xtoint);
        //---Debug.Log(ytoint);

        
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

        /*if (gd.GridTabVal.GetLength(0) <= objX + xtoint || gd.GridTabVal.GetLength(1) <= objY + ytoint || xtoint - objX < 0 || ytoint - objY < 0)
        {
            Debug.Log("ça a pété");
            Debug.Log(gd.GridTabVal.GetLength(0));
            Debug.Log(gd.GridTabVal.GetLength(1));
            return false;
        }*/
        Vector2 vec = gd.GetNearestRefOnGrid(new Vector3(xtoint,0f,ytoint));
        Debug.Log(gd.GridTabVal[(int)vec.x,(int)vec.y]);
        if(vec.x == -99999 || gd.GridTabVal[(int)vec.x,(int)vec.y] == 1.0f){

            return false;
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
            //Debug.Log(chaine);
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