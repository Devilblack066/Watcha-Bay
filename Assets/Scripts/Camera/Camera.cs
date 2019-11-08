using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject LeftPoint;
    public GameObject RightPoint;

    Vector3 VectorBetweenPoints;

    float CurrentPos = 0.5f;
    float speed = 0.2f;

    bool isTouching;
    // Start is called before the first frame update
    void Start()
    {
        VectorBetweenPoints = RightPoint.transform.position - LeftPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android && Application.platform == RuntimePlatform.IPhonePlayer)
        {
            TestTouch();
        }
        else
        {
            TestClick();
            //Debug.Log(isTouching);
        }
        TestCameraMove();
        MoveCamera();
    }


    //Test click sur ordi
    void TestClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isTouching = true;
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
        transform.position = new Vector3(result.x-3000, transform.position.y, transform.position.z);
    }
}

