﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swimmer : MonoBehaviour
{
    PatrolScript myPatrol;
    NavMeshAgent myAgent;
    ANeed[] myNeeds;
    Vector3 Destination;

    public bool isHighLighted = false;
    // Start is called before the first frame update
    void Start()
    {
        myPatrol = GetComponent<PatrolScript>();
        myAgent  = GetComponent<NavMeshAgent>();
        myNeeds = ANeed.generateBasicalNeeds();
        StartCoroutine(decreaseRessByTime(5.0f));
        Destination = myPatrol.FindAPointOnBeach();
        MoveTo();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2);
        if (Vector3.Distance(this.transform.position,Destination) < 500)
        {
            Destination = myPatrol.FindAPointOnBeach();
            MoveTo();
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


    IEnumerator decreaseRessByTime(float delay = 5.0f)
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
}
