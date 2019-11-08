using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swimmer : MonoBehaviour
{
    PatrolScript myPatrol;
    NavMeshAgent myAgent;

    Vector3 Destination;
    // Start is called before the first frame update
    void Start()
    {
        myPatrol = GetComponent<PatrolScript>();
        myAgent  = GetComponent<NavMeshAgent>();
        Destination = myPatrol.FindAPointOnBeach();
        MoveTo();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2);
        if (Vector3.Distance(this.transform.position,Destination) < 500)
        {
            Destination = myPatrol.FindAPointOnBeach();
            MoveTo();
        }
    }
    void MoveTo()
    {
        myAgent.SetDestination(Destination);
    }
}
