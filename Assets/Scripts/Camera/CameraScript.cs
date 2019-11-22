using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static bool onSomething;
    public GameObject LeftPoint;
    public GameObject RightPoint;

    Vector3 VectorBetweenPoints;

    float CurrentPos = 0.5f;
    float speed = 0.1f;

    bool isTouching;

    GameObject ObjectUnderMouse;

    public GameObject SwimmerWindow;
    // Start is called before the first frame update
    void Start()
    {
        VectorBetweenPoints = RightPoint.transform.position - LeftPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Le ray cast    
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            //Debug.Log(hit.collider.name);
            ObjectUnderMouse = hit.collider.gameObject;
        }
        else
        {
            ObjectUnderMouse = null;
        }

        // test si mobile ou PC
        if (Application.platform == RuntimePlatform.Android && Application.platform == RuntimePlatform.IPhonePlayer)
        {
            TestTouch();
        }
        else
        {
            TestClick();
            //Debug.Log(isTouching);
        }



        //Camera qui bouge
        TestCameraMove();
        MoveCamera();
    }


    //Test click sur ordi
    void TestClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && ((ObjectUnderMouse && (ObjectUnderMouse.tag == "Water" || ObjectUnderMouse.tag == "Floor")) || ObjectUnderMouse == null))
        {
            //SwimmerWindow.SetActive(false);
            isTouching = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && (ObjectUnderMouse && ObjectUnderMouse.tag == "Swimmer" ))
        {
            ShowSwimmerStat(ObjectUnderMouse);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isTouching = false;
        }
    }

    //test click 
    void TestTouch()
    {
        if (Input.touchCount > 0)
        {
            isTouching = true;
        }
        else
        {
            isTouching = false;
        }
    }


    void TestCameraMove()
    {
        if (isTouching)
        {
            if (Input.GetAxis("Mouse X") != 0)//
            {
                //Debug.Log(Input.GetAxis("Mouse X"));
                if(Input.GetAxis("Mouse X") > 0 && CurrentPos > 0)
                {
                    CurrentPos -= Input.GetAxis("Mouse X") * speed;
                }
                if (Input.GetAxis("Mouse X") < 0 && CurrentPos < 1)
                {
                    CurrentPos -= Input.GetAxis("Mouse X") * speed;
                }
            }
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    Debug.Log(touchDeltaPosition);
                    touchDeltaPosition = new Vector2(touchDeltaPosition.x, touchDeltaPosition.y);
                    
                }
            }
        }
    }
    void MoveCamera()
    {
        Vector3 result = LeftPoint.transform.position + (VectorBetweenPoints * CurrentPos);
        transform.position = new Vector3(result.x-25, transform.position.y, transform.position.z);
    }
    void ShowSwimmerStat(GameObject swimmer)
    {
        SwimmerWindow.SetActive(true);
        SwimmerWindow.GetComponent<SwimmerWindow>().swimmer = swimmer;
    }
}

