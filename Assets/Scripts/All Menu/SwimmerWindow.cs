using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmerWindow : MonoBehaviour
{
    public GameObject swimmer;
    public GameObject myCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (swimmer && swimmer.tag == "Swimmer")
        {
            //Debug.Log("????");
            transform.position = swimmer.transform.position;
           // transform.LookAt(myCamera.transform);
            //Debug.Log(transform.position);
        }
    }
}
