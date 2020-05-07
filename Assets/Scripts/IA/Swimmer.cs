using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swimmer : MonoBehaviour
{
    public enum SwimmerState { Wandering = 0, IsGoingToABuilding = 1, IsUsingABuilding = 2 }
    public BayStats bayStats;

    string firstName;
    string lastName;

    NavMeshAgent myAgent;
    ANeed[] myNeeds;
    Vector3 Destination;

    public Material poop;
    public Material divertissement;
    public Material cocktail;
    public Material hamburger;
    public Material happy;
    public Material Zzz;

    public ParticleSystem particles;
    public ParticleSystemRenderer particlesR;

    SwimmerState State = 0;

    public bool isHighLighted = false;

    public float timerInTheBay = 0.0f;

    public float cooldownDefaultSearch = 10.0f;
    float cooldownCurrentSearch = 0.0f;

    BuildingScript lastBuilding;

    public float cooldownDefaultUsingB = 3.0f;
    float cooldownUsingB = 0.0f;

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


        if (State == SwimmerState.Wandering)
        {
            cooldownCurrentSearch = cooldownCurrentSearch - Time.deltaTime;
            if (Vector3.Distance(this.transform.position, Destination) < 2 && bayStats)
            {
                Destination = bayStats.FindAPointOnBeach();
                MoveTo();
            }
            if (myNeeds[4].Value < 30)
            {
                leaveTheBay();
            }
            if (cooldownCurrentSearch <= 0.0f)
            {
                if (myNeeds[0].Value < 50)
                {
                    searchBuilding(myNeeds[0]);

                }

                if (myNeeds[1].Value < 98)
                {
                    searchBuilding(myNeeds[1]);
                }

                if (myNeeds[2].Value < 50)
                {
                    searchBuilding(myNeeds[2]);
                }

                if (myNeeds[3].Value < 50)
                {
                    searchBuilding(myNeeds[3]);
                }

                if (myNeeds[4].Value < 50)
                {
                    searchBuilding(myNeeds[4]);
                }

                if (myNeeds[5].Value < 50)
                {
                    searchBuilding(myNeeds[5]);
                }
            }
            transform.LookAt(Destination);

        }
        if (State == SwimmerState.IsGoingToABuilding)
        {
            Debug.Log("IS GOING !");
            if (Vector3.Distance(this.transform.position, Destination) < 2)
            {
                cooldownUsingB = cooldownDefaultUsingB;
                changeState(SwimmerState.IsUsingABuilding);
            }
        }

        if (State == SwimmerState.IsUsingABuilding)
        {
            cooldownUsingB -= Time.deltaTime;
            if (cooldownUsingB <= 0)
            {
                
                provideNeed(lastBuilding);
                changeState(SwimmerState.Wandering);

            }
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

    public void changeState(SwimmerState statut)
    {
        State = statut;
    }

    public void searchBuilding(ANeed need)
    {
        changeState(SwimmerState.IsGoingToABuilding);
        //Debug.Log("The swimmer need is " + need.Name);
        BuildingScript[] buildingTab = GameObject.FindObjectsOfType<BuildingScript>();
        if (buildingTab.Length == 0)
        {
            changeState(SwimmerState.Wandering);
            cooldownCurrentSearch = cooldownDefaultSearch;
            return;
        }
        foreach (BuildingScript building in buildingTab)
        {
            building.CatchBuildingBonus();
            /*Debug.Log(building.BuildName);
            Debug.Log(building.theBonus.Count);*/
            building.ShowBonus();
            if (building.theBonus.ContainsKey(need.Name) && (building.theBonus[need.Name].EnumMultiplier == BonusMultiplier.X1 || building.theBonus[need.Name].EnumMultiplier == BonusMultiplier.X2 || building.theBonus[need.Name].EnumMultiplier == BonusMultiplier.X3))
            {
                Destination = building.Entrance.transform.position;
                lastBuilding = building;
                MoveTo();
            }
        }

    }

    public void provideNeed(BuildingScript building)
    {
        foreach(KeyValuePair<string, BonusCorrespondance> theBonus in building.theBonus)
        { 
            Debug.Log("provideNeed " + theBonus.Key);
            Debug.Log("provideNeed price: " + theBonus.Value.Price);
            switch(theBonus.Value.EnumMultiplier)
            {
                case BonusMultiplier.X1:
                    addRessources(theBonus.Key, 10);
                    bayStats.SellSomething(theBonus.Value.Price);
                    break;
                case BonusMultiplier.X2:
                    addRessources(theBonus.Key, 20);
                    bayStats.SellSomething(theBonus.Value.Price);
                    break;
                case BonusMultiplier.X3:
                    addRessources(theBonus.Key, 30);
                    bayStats.SellSomething(theBonus.Value.Price);
                    break;
                case BonusMultiplier.M1:
                    addRessources(theBonus.Key, -10);
                    break;
                case BonusMultiplier.M2:
                    addRessources(theBonus.Key, -20);
                    break;
                case BonusMultiplier.M3:
                    addRessources(theBonus.Key, -30);
                    break;
                default:
                    break;

            }
        }
    }

    public void addRessources(string nameANeed, int value)
    {
        for (int i = 0; i < myNeeds.Length; ++i)
        {
            switch (nameANeed)
            {
                case "Happinness":
                    particlesR.material = happy;
                    particles.Play();
                    break;

                case "Hunger" :
                    particlesR.material = hamburger;
                    particles.Play();
                    break;

                case "Thirst":
                    particlesR.material = cocktail;
                    particles.Play();
                    break;

                case "Entertainment":
                    particlesR.material = divertissement;
                    particles.Play();
                    break;

                case "Tiredness":
                    particlesR.material = Zzz;
                    particles.Play();
                    break;

                case "Hygiene":
                    particlesR.material = poop;
                    particles.Play();
                    break;

                default:
                    break;
            }
            if (myNeeds[i].Name == nameANeed)
            {
                myNeeds[i].addValue(value);
                return;
            }
        }
    }

    public string getLastName() { return lastName; }
    public string getFirstName() { return firstName; }
}
