using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    

    [SerializeField]
    private int size = 1;

    [SerializeField]
    private int gridWorldSizeX = 50;

    [SerializeField]
    private int gridWorldSizeY = 50;

    public float[,] GridTabVal;
    public Vector3[,] GridTabPos;

    private int x1;
    private int z1;
    
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


    void Awake()
    {
        GridTabVal = new float [gridWorldSizeX, gridWorldSizeY];
        GridTabPos = new Vector3[gridWorldSizeX*size,gridWorldSizeY*size];

        for (float x = 0; x < gridWorldSizeX; x += size)
        {
            GridTabVal[x1, 0]= 0;   
    
            for (float z = 0; z < gridWorldSizeY; z += size)
            {
                GridTabVal[x1, z1]= 0;
                GridTabPos[x1, z1]= new Vector3(x, 0f, z) + transform.position;
                //Debug.Log(GridTabPos[x1, z1]);
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
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z) + transform.position);
                Gizmos.DrawWireCube(point,new Vector3(0.1f*size,0.1f*size,0.1f*size));
                //Debug.Log(GridTabPos[x1, z1]);
                z1 += 1;
            }

            x1 += 1;
            z1 =0;
        }
    }
}
