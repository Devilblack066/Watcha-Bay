using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwimmerWindow : MonoBehaviour
{
    public GameObject swimmer;
    public GameObject myCamera;
    public Slider mySliderHappiness;
    public Slider mySliderHunger;
    public Slider mySliderThirst;
    public Slider mySliderEntertainment;
    public Slider mySliderTiredness;
    public Slider mySliderHygiene;

    public TextMeshProUGUI lastName;
    public TextMeshProUGUI firstName;

    public float result;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (swimmer && swimmer.tag == "Swimmer")
        {
            ANeed[] tab_needs = swimmer.GetComponent<Swimmer>().GetmyNeeds();

            lastName.text = swimmer.GetComponent<Swimmer>().getLastName();
            firstName.text = swimmer.GetComponent<Swimmer>().getFirstName();
            //ANeed[] tab_needs = swimmer.GetComponent<Swimmer>().myNeeds;
            //Debug.Log("????");
            //transform.position = swimmer.transform.position;
            // transform.LookAt(myCamera.transform);
            //Debug.Log(transform.position);
            foreach (ANeed need in tab_needs)
            {
                result = ((float)need.Value / (float)need.MaxValue);
                tab_needs = swimmer.GetComponent<Swimmer>().GetmyNeeds();
                //Debug.Log("Nom:" + need.Name);
                switch (need.Name)
                {
                    case "Happiness":
                        mySliderHappiness.value = result;
                        //Debug.Log("Happiness a pour valeur " + need.Value + "/" + need.MaxValue);
                        break;
                    case "Hunger":
                        mySliderHunger.value = result;
                        //Debug.Log("Hunger a pour valeur " + need.Value + "/" + need.MaxValue);
                        break;
                    case "Thirst":
                        mySliderThirst.value = result;
                        Debug.Log("Thrist a pour valeur " + need.Value + "/" + need.MaxValue);
                        break;
                    case "Entertainment":
                        mySliderEntertainment.value = result;
                        //Debug.Log("Entertainment a pour valeur " + need.Value + "/" + need.MaxValue);
                        break;
                    case "Tiredness":
                        mySliderTiredness.value = result;
                        //Debug.Log("Tiredness a pour valeur " + need.Value + "/" + need.MaxValue);
                        break;
                    case "Hygiene":
                        mySliderHygiene.value = result;
                        break;
                    default:
                        //Debug.Log("Valeur non récupérée: "+ result);
                        break;
                }
            }
        }
        else if (swimmer == null && gameObject.active == true)
        {
            gameObject.SetActive(false);
        }
    }
    public void HideMenu()
    {
        gameObject.SetActive(false);
    }
}
