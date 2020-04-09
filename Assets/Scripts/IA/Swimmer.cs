using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swimmer : MonoBehaviour
{
    enum SwimmerState { Wandering, IsGoingToABuilding, IsUsingABuilding }
    public BayStats bayStats;

    string firstName;
    string lastName;

    NavMeshAgent myAgent;
    ANeed[] myNeeds;
    Vector3 Destination;

    SwimmerState State = 0;

    public bool isHighLighted = false;

    public float timerInTheBay = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //myPatrol = GetComponent<PatrolScript>();
        myAgent  = GetComponent<NavMeshAgent>();
        myAgent.updateRotation = false;


        //Debug.Log(generalFunctions.allLastNames.Count);
        lastName = generalFunctions.allLastNames.ToArray()[Random.Range(0,generalFunctions.allLastNames.Count)];
        firstName = generalFunctions.allFirstNames.ToArray()[Random.Range(0, generalFunctions.allFirstNames.Count)];
        gameObject.name = lastName+" "+firstName;

        myNeeds = ANeed.generateBasicalNeeds();
        StartCoroutine(decreaseRessByTime(4.0f));
        Destination = bayStats.FindAPointOnBeach();
        MoveTo();
    }

    public ANeed[] GetmyNeeds()
    {
        return (myNeeds);
    }

    // Update is called once per frame
    void Update()
    {
        timerInTheBay += Time.deltaTime;
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2);


        if (State == 0)
        {
            if (Vector3.Distance(this.transform.position, Destination) < 2 && bayStats)
            {
                Destination = bayStats.FindAPointOnBeach();
                MoveTo();
            }
            if (myNeeds[4].Value < 30)
            {
                leaveTheBay();
            }
            if (myNeeds[0].Value < 98)
            {
                State = SwimmerState.IsGoingToABuilding;
                SearchBuilding(myNeeds[0]);
            }

            if (myNeeds[1].Value < 98)
            {
                State = SwimmerState.IsGoingToABuilding;
                SearchBuilding(myNeeds[1]);
            }

            if (myNeeds[2].Value < 50)
            {
                State = SwimmerState.IsGoingToABuilding;
                SearchBuilding(myNeeds[2]);
            }

            if (myNeeds[3].Value < 50)
            {
                State = SwimmerState.IsGoingToABuilding;
                SearchBuilding(myNeeds[3]);
            }

            if (myNeeds[4].Value < 50)
            {
                State = SwimmerState.IsGoingToABuilding;
                SearchBuilding(myNeeds[4]);
            }

            if (myNeeds[5].Value < 50)
            {
                State = SwimmerState.IsGoingToABuilding;
                SearchBuilding(myNeeds[5]);
            }

            transform.LookAt(Destination);
        }
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            showNeeds();
        }*/
    }
    void MoveTo()
    {
        myAgent.SetDestination(Destination);
    }
    void showNeeds()
    {
        foreach (ANeed need in myNeeds)
        {
            Debug.Log("Pour " + need.Name + " a pour valeur " + need.Value + "/" + need.MaxValue);
        }
    }


    IEnumerator decreaseRessByTime(float delay = 4.0f)
    {
        while (true)
        {
            for (int i = 0;i<myNeeds.Length;++i)
            {
                if (myNeeds[i].Value > 0)
                {
                    myNeeds[i].Value -= 1;
                }
            }
            //Debug.Log("?");
            yield return new WaitForSeconds(delay);
        }
    }
    public void setBay(BayStats tb)
    {
        bayStats = tb;
    }

    public void leaveTheBay()
    {
        bayStats.leaveTheBay(gameObject);
        Destroy(gameObject);
    }

    public void SearchBuilding(ANeed need)
    {
        Debug.Log("The need is " + need.Name);
    }

    public string getLastName() { return lastName; }
    public string getFirstName() { return firstName; }
}
