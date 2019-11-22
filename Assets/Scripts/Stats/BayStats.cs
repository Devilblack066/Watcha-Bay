using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayStats : MonoBehaviour
{
    int currentSwimmer = 0;
    int maxSwimmer = 5;

    public List<GameObject> AllFloors;
    //public GameObject goal;
    List<GameObject> CatchedGOsBeach;
    List<GameObject> CatchedGOsWater;

    GameObject[] AllwalkablesBeach;
    GameObject[] AllwalkablesWater;

    //Vector3 Destination;


    //public GameObject TheMesh;

    List<GameObject> AllSwimmers;
    // Start is called before the first frame update
    void Awake()
    {
        AllSwimmers = new List<GameObject>();
        FindAllWalkablesFloor();
        //FindAPoint();
        //FindAPointOnBeach();
        //GetComponent<NavMeshAgent>().SetDestination();
        //Debug.Log(Allwalkables.Length);
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<NavMeshAgent>().Move(goal.transform.position);
        //Debug.Log(Vector3.Distance(transform.position, Destination));
        /*if (Vector3.Distance(transform.position,Destination) < 500)
        {
            //Debug.Log("Ouais");
            FindAPointOnBeach();
        }*/
    }
    public Vector3 FindAPointOnBeach()
    {
        GameObject goToWalk = AllwalkablesBeach[Random.Range(0, AllwalkablesBeach.Length)];
        //Debug.Log("The X : "+ Random.Range(goToWalk.transform.position.x - goToWalk.transform.localScale.x , goToWalk.transform.localScale.x + goToWalk.transform.localScale.x ));
        return new Vector3(Random.Range(goToWalk.transform.position.x - goToWalk.transform.localScale.x * 4, goToWalk.transform.localScale.x + goToWalk.transform.localScale.x * 4)
                                 , goToWalk.transform.position.y + transform.localScale.y/2
                                 , Random.Range(goToWalk.transform.position.z - goToWalk.transform.localScale.z * 4, goToWalk.transform.position.z + goToWalk.transform.localScale.z * 4));
        //GetComponent<NavMeshAgent>().SetDestination(Destination);
    }
    public Vector3 FindAPointOnWater()
    {
        GameObject goToWalk = AllwalkablesWater[Random.Range(0, AllwalkablesBeach.Length)];
        Debug.Log(goToWalk.transform.position.x - goToWalk.transform.localScale.x * 4 +"         " +goToWalk.transform.localScale.x + goToWalk.transform.localScale.x * 4);
        return new Vector3(Random.Range(goToWalk.transform.position.x - goToWalk.transform.localScale.x * 4, goToWalk.transform.localScale.x + goToWalk.transform.localScale.x * 4)
                                 , goToWalk.transform.position.y + transform.localScale.y/2
                                 , Random.Range(goToWalk.transform.position.z - goToWalk.transform.localScale.z * 4, goToWalk.transform.position.z + goToWalk.transform.localScale.z * 4));
        //GetComponent<NavMeshAgent>().SetDestination(Destination);
    }
    void FindAllWalkablesFloor()
    {
        CatchedGOsBeach = new List<GameObject>();
        CatchedGOsWater = new List<GameObject>();
        foreach (GameObject go in AllFloors)
        {
            if (go.GetComponent<AWalkable>())
            {
                AWalkable awalkable = go.GetComponent<AWalkable>();
                if (awalkable.IsUnlock == true && awalkable.theType == typeOfWalkable.Sand)
                {
                    CatchedGOsBeach.Add(go);
                }
                if (awalkable.IsUnlock == true && awalkable.theType == typeOfWalkable.Water)
                {
                    CatchedGOsWater.Add(go);
                }
            }
        }
        AllwalkablesBeach = CatchedGOsBeach.ToArray();
        AllwalkablesWater = CatchedGOsBeach.ToArray();
    }

    public void addSwimmer(GameObject swimmer)
    {
        ++currentSwimmer;
        AllSwimmers.Add(swimmer);
    }
    public bool canAddSwimmer()
    {
        if (currentSwimmer < maxSwimmer) return true;
        else return false;
    }
    public void leaveTheBay(GameObject swimmer)
    {
        --currentSwimmer;
        AllSwimmers.Remove(swimmer);
    }
}
