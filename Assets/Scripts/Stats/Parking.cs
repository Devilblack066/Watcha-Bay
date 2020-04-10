using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parking : MonoBehaviour
{
    BayStats theBay;

    public GameObject swimmerPrefab;
    public GameObject carPrefab;
    public GameObject Entrance;


    [SerializeField]
    private int size = 1;

    [SerializeField]
    private int gridWorldSizeX = 50;

    [SerializeField]
    private int gridWorldSizeY = 50;

    public GameObject[][] CarStored;
    public Vector3[][] GridTabPos;

    private int x1;
    private int z1;


    void Awake()
    {

        CarStored = new GameObject[gridWorldSizeX / size][];
        GridTabPos = new Vector3[gridWorldSizeX / size][];

        //Debug.Log(gridWorldSizeY / size);
        for (int i = 0; i < gridWorldSizeX/size; ++i)
        {
            GridTabPos[i] = new Vector3[gridWorldSizeY / size];
            CarStored[i] = new GameObject[gridWorldSizeY / size];
        }

        Vector3 initialpos = transform.position;

        for (int x = 0; x < gridWorldSizeX / size; ++x)
        {
            CarStored[x][0] = null;

            for (int z = 0; z < gridWorldSizeY / size; ++z)
            {
                CarStored[x][z] = null;
                GridTabPos[x][z] = new Vector3((x*size) + initialpos.x, 0f, (z*size) + initialpos.z);

                //Debug.Log(GridTabPos[x1][z1]);
                //z1 += 1;
            }
        }

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

    public void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        x1 = 0;
        z1 = 0;
        for (float x = 0; x < gridWorldSizeX; x += size)
        {
            for (float z = 0; z < gridWorldSizeY; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z) + transform.position);
                Gizmos.DrawWireCube(point, new Vector3(0.1f * size, 0.1f * size, 0.1f * size));
                //Debug.Log(GridTabPos[x1, z1]);
                z1 += 1;
            }

            x1 += 1;
            z1 = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        theBay = GetComponentInParent<BayStats>();
        StartCoroutine(spawnSwimmers(4.0f));
        //Debug.Log(" tu es dans le start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator spawnSwimmers(float delay)
    {
        while (true)
        {
            //Debug.Log(" tu es dans le while");
            if (theBay.canAddSwimmer())
            {
                
                GameObject go = Instantiate(swimmerPrefab,Entrance.transform.position, Entrance.transform.rotation,null);
                theBay.addSwimmer(go);
                placeACar();
                go.GetComponent<Swimmer>().setBay(theBay);
            }
            yield return new WaitForSeconds(delay);
        }
    }
    void placeACar()
    {
        while (true)
        {
            Debug.Log(CarStored.Length);
            int line = Random.Range(0, CarStored.Length);
            int column = Random.Range(0, CarStored[line].Length);
            Debug.Log(CarStored[line].Length);
            if (CarStored[line][column] == null)
            {
                Debug.Log(GridTabPos[line][column]);
                CarStored[line][column] = Instantiate(carPrefab, GridTabPos[line][column], Entrance.transform.rotation, this.transform);
                return;
            }
        }
    }
}
