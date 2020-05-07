using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CameraScript : MonoBehaviour
{

    public enum StateEnum
    {
        Normal,
        Construction,
        Destruction,
    }

    public StateEnum Typestate;

    //public static bool inConstructionMode = false;
    //public static bool inDestructionMode = false;
    public ObjectPositioner myObjectPositioner;
    public ConstructionWindow theConstructionWindow;

     public static bool onSomething;
     public GameObject LeftPoint;
     public GameObject RightPoint;
     public GameObject UpPoint;
     public GameObject DownPoint;
     public float ZoomSpeed = 10.0f;

     Vector3 VectorBetweenPointsX;
     Vector3 VectorBetweenPointsY;

     float CurrentPosX;
     float CurrentPosY;

     public GraphicRaycaster m_Raycaster;
     public PointerEventData m_PointerEventData;
     public EventSystem m_EventSystem;
     bool onUI = false;

     bool isTouching;

     GameObject ObjectUnderMouse;

     public GameObject SwimmerWindow;
    public GameObject oldswimmer;

    public AudioSource BeachSound;

    BayStats theBay;

    Vector2?[] oldTouchPositions = {
        null,
        null
    };
    Vector2 oldTouchVector;
    float oldTouchDistance;
    public float CameraSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
         CurrentPosX = 0.5f;
         CurrentPosY = 0.0f;
         //VectorBetweenPointsX = RightPoint.transform.position - LeftPoint.transform.position;
         // VectorBetweenPointsY = UpPoint.transform.position - DownPoint.transform.position;
         Typestate = StateEnum.Normal;
         myObjectPositioner = gameObject.GetComponent<ObjectPositioner>();
         //theConstructionWindow = GameObject.FindObjectOfType<ConstructionWindow>();
         theBay = GameObject.FindObjectOfType<BayStats>();
         //m_Raycaster = GetComponent<GraphicRaycaster>();
         //Debug.Log(VectorBetweenPointsX);
         //Debug.Log(VectorBetweenPointsY);
    }

    public void setEnumState(StateEnum value)
    {
        Typestate = value;
    }


    void ZoomCamera(float Axis)
    {
        Axis = Mathf.Clamp(Axis, -1, 1);
        Vector3 NewPos;
        NewPos = transform.forward * ZoomSpeed * Axis;
        //Debug.Log(transform.forward.z * ZoomSpeed * 10);
        transform.position = new Vector3(transform.position.x + NewPos.x, Mathf.Clamp(transform.position.y + NewPos.y, 2.0f,40.0f), transform.position.z + NewPos.z);

    }
    /*
    // Update is called once per frame
    void Update()
    {
        //Create a list of Raycast Results

        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if(results.Count > 0)onUI = true;
        else onUI = false;
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
            TestClick(hit.point);
            //Debug.Log(isTouching);
        }

        //Debug.Log(CurrentPosY);
        BeachSound.volume = CurrentPosY-0.2f;
        GetComponent<AudioSource>().volume = 1 - CurrentPosY-0.30f;
        //Camera qui bouge
        TestCameraMove();
        MoveCamera();
    }

   */
    //Test click sur ordi et pour l'éditeur Unity
    void TestClick(Vector3 hitpoint)
    {

        GameObject ObjHited = ObjUnderRay();
        if (onUI) return;
         if (Typestate == StateEnum.Construction && Input.GetKeyDown(KeyCode.Mouse0))
         {
            if (theConstructionWindow && theConstructionWindow.SelectedBuild != null)
            {
                if (theBay.CanYouPay(theConstructionWindow.SelectedBuild.GetComponent<BuildingIcon>().Prefab.GetComponent<BuildingScript>().Price))
                {
                    myObjectPositioner.PlaceCubeNear(hitpoint, theConstructionWindow.SelectedBuild.GetComponent<BuildingIcon>().Prefab, theBay, theConstructionWindow.SelectedBuild.GetComponent<BuildingIcon>().Prefab.GetComponent<BuildingScript>().Price);
                }
            }
            else Debug.Log("Pas de batiment sélectionné");
         }
        else if (Typestate == StateEnum.Destruction  && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (ObjectUnderMouse.tag == "batiments")
            {
                Debug.Log(ObjectUnderMouse);
                myObjectPositioner.SellCubeNear(hitpoint, ObjectUnderMouse, theBay, ObjHited.GetComponent<BuildingScript>().Price);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && ((ObjectUnderMouse && (ObjectUnderMouse.tag == "Water" || ObjectUnderMouse.tag == "Floor")) || ObjectUnderMouse == null))
         {
             //SwimmerWindow.SetActive(false);
             isTouching = true;
         }
         else if (Input.GetKeyDown(KeyCode.Mouse0) && (ObjectUnderMouse && ObjectUnderMouse.tag == "Swimmer"))
         {
             ShowSwimmerStat(ObjectUnderMouse);
         }
         if (Input.GetKeyUp(KeyCode.Mouse0))
         {
             isTouching = false;
         }
         if(Input.GetAxis("Mouse ScrollWheel") != 0)
         {
            ZoomCamera(Input.GetAxis("Mouse ScrollWheel") * 10.0f);
         }
    }

     //test click pour le téléphone
     void TestTouch()
     {
        GameObject ObjHited = ObjUnderRay();
        if (Input.touchCount == 0)
        {
            oldTouchPositions[0] = null;
            oldTouchPositions[1] = null;
            onUI = false;
            ObjectUnderMouse = null;
        }
        else if (Input.touchCount == 1)
        {
            TestIfOnUI();
            if (onUI) return;


            Vector3 posOfHit = TestUnderRay();

            if (Typestate == StateEnum.Construction)
            {
                if (theConstructionWindow && theConstructionWindow.SelectedBuild != null)
                {
                    if (theBay.CanYouPay(theConstructionWindow.SelectedBuild.GetComponent<BuildingIcon>().Prefab.GetComponent<BuildingScript>().Price))
                    {
                        //theBay.PaySomething(theConstructionWindow.SelectedBuild.GetComponent<BuildingIcon>().Prefab.GetComponent<BuildingScript>().Price);
                        myObjectPositioner.PlaceCubeNear(posOfHit, theConstructionWindow.SelectedBuild.GetComponent<BuildingIcon>().Prefab,theBay, theConstructionWindow.SelectedBuild.GetComponent<BuildingIcon>().Prefab.GetComponent<BuildingScript>().Price);
                    }
                }
                else Debug.Log("Pas de batiment sélectionné");
            }
            else if (Typestate == StateEnum.Destruction)
            {
                if (ObjectUnderMouse.tag == "batiments")
                {
                    Debug.Log(ObjectUnderMouse);
                    myObjectPositioner.SellCubeNear(posOfHit, ObjectUnderMouse, theBay, ObjHited.GetComponent<BuildingScript>().Price);
                }
            }
            else if((ObjectUnderMouse && ObjectUnderMouse.tag == "Swimmer")) ShowSwimmerStat(ObjectUnderMouse);
            else if (oldTouchPositions[0] == null || oldTouchPositions[1] != null)
            {
                oldTouchPositions[0] = Input.GetTouch(0).position;
                oldTouchPositions[1] = Input.GetTouch(1).position;
            }
            else
            {
                Vector2 newTouchPosition = Input.GetTouch(0).position;

                Vector3 oldTouchVectortransform = new Vector3(oldTouchPositions[0].Value.x,0, oldTouchPositions[0].Value.y);
                Vector3 newTouchVectortransform = new Vector3(newTouchPosition[0], 0, newTouchPosition[1]);

                transform.position += (oldTouchVectortransform - newTouchVectortransform) * GetComponent<Camera>().orthographicSize / GetComponent<Camera>().pixelHeight * CameraSpeed;

                oldTouchPositions[0] = newTouchPosition;
            }
        }
        else
        {
            if (oldTouchPositions[1] == null)
            {
                oldTouchPositions[0] = Input.GetTouch(0).position;
                oldTouchPositions[1] = Input.GetTouch(1).position;
                oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
                oldTouchDistance = oldTouchVector.magnitude;
            }
            else
            {
                Vector2 screen = new Vector2(GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight);

                Vector2[] newTouchPositions = {
                    Input.GetTouch(0).position,
                    Input.GetTouch(1).position
                };
                Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
                Vector2 oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
                float newTouchDistance = newTouchVector.magnitude;
                float oldTouchDistance = oldTouchVector.magnitude;

                ZoomCamera(-1+(newTouchDistance/oldTouchDistance));
                
                /*transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] + oldTouchPositions[1] - screen) * GetComponent<Camera>().orthographicSize / screen.y));
                transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, Mathf.Asin(Mathf.Clamp((oldTouchVector.y * newTouchVector.x - oldTouchVector.x * newTouchVector.y) / oldTouchDistance / newTouchDistance, -1f, 1f)) / 0.0174532924f));
                GetComponent<Camera>().orthographicSize *= oldTouchDistance / newTouchDistance;
                transform.position -= transform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * GetComponent<Camera>().orthographicSize / screen.y);*/

               /* oldTouchPositions[0] = newTouchPositions[0];
                oldTouchPositions[1] = newTouchPositions[1];
                oldTouchVector = newTouchVector;
                oldTouchDistance = newTouchDistance;*/

            }
        }
    }
   
    //Test sur le déplacement de la caméra sur l'ordinateur
     void TestCameraMove()
     {
         if (isTouching)
         {
             if (Input.GetAxis("Mouse X") != 0)//
             {
                   transform.position -= Input.GetAxis("Mouse X") * CameraSpeed * transform.right;
             }
             if (Input.GetAxis("Mouse Y") != 0)//
             {
                   transform.position -= Input.GetAxis("Mouse Y") * CameraSpeed * transform.parent.transform.forward;
             }
         }
     }

    //déplacement de caméra sur l'ordinateur
     void MoveCamera()
     {
         Vector3 result = (LeftPoint.transform.position + (VectorBetweenPointsX * CurrentPosX)) + (DownPoint.transform.position + (VectorBetweenPointsY * CurrentPosY));
         transform.position = new Vector3(result.x, transform.position.y, result.z-25);
     }

    //Montre la fenêtre du nageur
    void ShowSwimmerStat(GameObject swimmer)
    {
        if(oldswimmer != null)
        {
            oldswimmer.transform.GetChild(1).gameObject.SetActive(false);
        }
        swimmer.transform.GetChild(1).gameObject.SetActive(true);
        SwimmerWindow.SetActive(true);
        SwimmerWindow.GetComponent<SwimmerWindow>().swimmer = swimmer;
        oldswimmer = swimmer;
    }

    //Savoir si la souris ou le doigt est sur de l'UI ou non
    void TestIfOnUI()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0) onUI = true;
        else onUI = false;
    }

    void Update()
    {
        // test si mobile ou PC
        //float calculForMusic = (transform.position.z - DownPoint.transform.position.z) / (UpPoint.transform.position.z - DownPoint.transform.position.z);
        //Debug.Log(calculForMusic);
        if (Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.WindowsEditor)
        {
            TestTouch();
            // Debug.Log(CurrentPosY);
            //CurrentPosY = calculForMusic + 0.2f;
            //Debug.Log(CurrentPosY);
        }
        else
        {
            TestIfOnUI();
            // Le ray cast    
            /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider.name);
                ObjectUnderMouse = hit.collider.gameObject;
            }
            else
            {
                ObjectUnderMouse = null;
            }*/
            TestClick(TestUnderRay());
            //Debug.Log(isTouching);
            TestCameraMove();
            //MoveCamera();
        }
       /* BeachSound.volume = CurrentPosY - 0.2f;
        GetComponent<AudioSource>().volume = 1 - CurrentPosY - 0.30f;*/
        //Camera qui bouge
    }

    Vector3 TestUnderRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.collider.name);
            ObjectUnderMouse = hit.collider.gameObject;
        }
        else
        {
            ObjectUnderMouse = null;
        }
        return hit.point;
    }

    GameObject ObjUnderRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.collider.name);
            ObjectUnderMouse = hit.collider.gameObject;
        }
        else
        {
            ObjectUnderMouse = null;
        }
        return ObjectUnderMouse;
    }
}

