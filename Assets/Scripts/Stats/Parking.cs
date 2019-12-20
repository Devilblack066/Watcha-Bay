using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parking : MonoBehaviour
{
    BayStats theBay;

    public GameObject swimmerPrefab;


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
                
                GameObject go = Instantiate(swimmerPrefab,transform.position,transform.rotation,null);
                theBay.addSwimmer(go);
                go.GetComponent<Swimmer>().setBay(theBay);
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
