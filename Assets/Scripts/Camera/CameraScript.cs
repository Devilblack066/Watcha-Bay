using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static bool onSomething;
    public GameObject LeftPoint;
    public GameObject RightPoint;
    public GameObject UpPoint;
    public GameObject DownPoint;

    Vector3 VectorBetweenPointsX;
    Vector3 VectorBetweenPointsY;

    float CurrentPosX;
    float CurrentPosY;
    float speed = 0.05f;

    

    bool isTouching;

    GameObject ObjectUnderMouse;

    public GameObject SwimmerWindow;

    public AudioSource BeachSound;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPosX = 0.5f;
        CurrentPosY = 0.5f;
        VectorBetweenPointsX = RightPoint.transform.position - LeftPoint.transform.position;
        VectorBetweenPointsY = UpPoint.transform.position - DownPoint.transform.position;
        Debug.Log(VectorBetweenPointsX);
        Debug.Log(VectorBetweenPointsY);
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
            //TestTouch();
        }
        else
        {
            TestClick();
            //Debug.Log(isTouching);
        }

        //Debug.Log(CurrentPosY);
        BeachSound.volume = CurrentPosY-0.2f;
        GetComponent<AudioSource>().volume = 1 - CurrentPosY-0.30f;
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
                if(Input.GetAxis("Mouse X") > 0 && CurrentPosX > 0)
                {
                    CurrentPosX -= Input.GetAxis("Mouse X") * speed;
                }
                if (Input.GetAxis("Mouse X") < 0 && CurrentPosX < 1)
                {
                    CurrentPosX -= Input.GetAxis("Mouse X") * speed;
                }
            }
            if (Input.GetAxis("Mouse Y") != 0)//
            {
                //Debug.Log(Input.GetAxis("Mouse X"));
                if (Input.GetAxis("Mouse Y") > 0 && CurrentPosY > 0)
                {
                    CurrentPosY -= Input.GetAxis("Mouse Y") * speed;
                }
                if (Input.GetAxis("Mouse Y") < 0 && CurrentPosY < 1)
                {
                    CurrentPosY -= Input.GetAxis("Mouse Y") * speed;
                }
               //Debug.Log(Input.GetAxis("Mouse Y"));
            }
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    //Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    //Debug.Log(touchDeltaPosition);
                    //touchDeltaPosition = new Vector2(touchDeltaPosition.x, touchDeltaPosition.y);
                    
                }
            }
        }
    }
    void MoveCamera()
    {
        Vector3 result = (LeftPoint.transform.position + (VectorBetweenPointsX * CurrentPosX)) + (DownPoint.transform.position + (VectorBetweenPointsY * CurrentPosY));
        transform.position = new Vector3(result.x, transform.position.y, result.z-25);
    }
    void ShowSwimmerStat(GameObject swimmer)
    {
        SwimmerWindow.SetActive(true);
        SwimmerWindow.GetComponent<SwimmerWindow>().swimmer = swimmer;
    }
}

