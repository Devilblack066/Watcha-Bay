using System.Collections;
using System.Collections.Generic;
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
        gd = FindObjectOfType<GridScript>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                    PlaceCubeNear(hitInfo.point,batiments);
            }
        }
    }
 
    public void PlaceCubeNear(Vector3 clickPoint,GameObject batiment)
    {



            var finalPosition = gd.GetNearestPointOnGrid(clickPoint);
                xtoint = (int) finalPosition.x;
                ytoint = (int) finalPosition.z;

            if(gd.GridTabVal[xtoint,ytoint] == 0f){
             objetinst = Instantiate(batiment,clickPoint,Quaternion.identity);
             objetinst.transform.position = finalPosition;
             SpawnBatGridTab(objetinst,xtoint,ytoint);
             actualbat = objetinst;
            }else if(gd.GridTabVal[xtoint,ytoint] == 1f){
                Debug.Log("Chris tu veut poser le batiment sur ste case mais quelle est deja prise");
            }
             

    }


    public void SpawnBatGridTab(GameObject go,int xtoint , int ytoint)
    {
        var objX=go.transform.localScale.x;
        var objY=go.transform.localScale.z;

        //Debug.Log((int)Mathf.Round(xtoint+objX));
        //Debug.Log((int)Mathf.Round(ytoint+objY));

        var Pointertogrid = gd.GridTabPos[xtoint,ytoint];

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
            
        }

    }

}