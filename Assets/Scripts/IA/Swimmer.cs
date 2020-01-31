using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swimmer : MonoBehaviour
{
    public BayStats bayStats;

    string firstName;
    string lastName;

    NavMeshAgent myAgent;
    ANeed[] myNeeds;
    Vector3 Destination;

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
        if (Vector3.Distance(this.transform.position,Destination) < 2 && bayStats)
        {
            Destination = bayStats.FindAPointOnBeach();
            MoveTo();
        }
        if (myNeeds[4].Value < 30)
        {
            leaveTheBay();
        }

        transform.LookAt(Destination);

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

    public string getLastName() { return lastName; }
    public string getFirstName() { return firstName; }
}
