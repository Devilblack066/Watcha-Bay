using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAtStart : MonoBehaviour
{

        GameObject obj;
        Collider other;
        ObjectPositioner ObjPos; 
        int isAlColided = 0;
    // Start is called before the first frame update
    void Start()
    {
        obj =  this.gameObject;


    }

  void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
 
    }
    private void OnTriggerEnter(Collider other) {

        /*if(other.gameObject.CompareTag("batiments") && isAlColided == 0)
        {
            Destroy(obj);
        }
        isAlColided = 1;*/
    }
    
}
