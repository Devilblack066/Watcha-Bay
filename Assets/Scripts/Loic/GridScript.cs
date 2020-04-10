using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject GridCell;

    GameObject GridRef;

    [SerializeField]
    public int size = 1;

    [SerializeField]
    public int gridWorldSizeX = 50;

    [SerializeField]
    public int gridWorldSizeY = 50;

    public float[,] GridTabVal;
    public Vector3[][] GridTabPos;
    public GameObject[,] GridTabGo;

    private int x1;
    private int z1;

    public void Update()
    {
       
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size, 
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    public Vector2 GetNearestRefOnGrid(Vector3 position)
    {
        Vector2 vec = new Vector2(-99999,0);
    

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size, 
            (float)zCount * size);

        result += transform.position;

        for(int i = 0 ; i < GridTabPos.Length ;i++){
            for(int j=0 ; j < GridTabPos[i].Length ; j++){

                //Debug.Log("TabPos -> "+ GridTabPos[i][j] + "; Postion à checker -> "+position);
                if(GridTabPos[i][j] == position){
                    //Debug.Log("C'est égal");
                    vec = new Vector2(i,j);
                }
            }
        } 
        return vec;
    }


    void Awake()
    {
        x1 =0;
        z1 =0;

        GridTabVal = new float [gridWorldSizeX, gridWorldSizeY];
        GridTabPos = new Vector3[gridWorldSizeX][];
        GridTabGo = new GameObject[gridWorldSizeX, gridWorldSizeY];
        for (int i = 0 ; i<gridWorldSizeX; ++i ){
            GridTabPos[i] =  new Vector3[gridWorldSizeY];
        }

        Vector3 initialpos = transform.position;

        for (float x = 0; x < gridWorldSizeX; x += size)
        {
            GridTabVal[x1, 0]= 0;   
    
            for (float z = 0; z < gridWorldSizeY; z += size)
            {
                GridTabVal[x1, z1]= 0;
                GridTabPos[x1][z1]= new Vector3(x+initialpos.x, 0f, z+initialpos.z) ;
                GridRef = Instantiate(GridCell, new Vector3(x + initialpos.x, 0.5f, z + initialpos.z), Quaternion.Euler(90, 0, 0), GameObject.Find("GridCells").transform);
                GridTabGo[x1,z1] = GridRef.gameObject;
                GridRef.gameObject.SetActive(false);
                //Debug.Log(GridTabGo[x1,z1]);
                z1 += 1;
            }
            x1 += 1;
            z1 =0;
        }
        
    }

    public void OnDrawGizmos()
    {
       
        Gizmos.color = Color.green;
        x1 =0;
        z1 =0;
        for (float x = 0; x < gridWorldSizeX; x += size)
        {  
            for (float z = 0; z < gridWorldSizeY; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z)+transform.position );
                Gizmos.DrawWireCube(point,new Vector3(0.1f*size,0.1f*size,0.1f*size));

                
                //Debug.Log(GridTabPos[x1, z1]);
                z1 += 1;
            }

            x1 += 1;
            z1 =0;
        }
    }
}
