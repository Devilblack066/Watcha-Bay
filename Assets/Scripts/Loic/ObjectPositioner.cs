using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPositioner : MonoBehaviour
{
    private Grid grid;

    Ray ray;
    RaycastHit hitInfo;

[SerializeField]
    public GameObject batiments;

    //[SerializeField]
    //public Camera cam;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if(hitInfo.collider.CompareTag("batiments"))
                {
                    Debug.Log("il ya deja un batiment");
                }else{
    
                PlaceCubeNear(hitInfo.point,batiments);

                }
            }
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint,GameObject batiment)
    {
        
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        var objetinst = Instantiate(batiment,clickPoint,Quaternion.identity);
        objetinst.transform.position = finalPosition;

    }

    private void CheckAllSize(Vector3 clickPoint,GameObject batiment)
    {

    }

}