using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CameraScript : MonoBehaviour
{
     public static bool inConstructionMode = false;
     public ObjectPositioner myObjectPositioner;
    public ConstructionWindow theConstructionWindow;

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

     public GraphicRaycaster m_Raycaster;
     public PointerEventData m_PointerEventData;
     public EventSystem m_EventSystem;
     bool onUI = false;

     bool isTouching;

     GameObject ObjectUnderMouse;

     public GameObject SwimmerWindow;

     public AudioSource BeachSound;


    Vector2?[] oldTouchPositions = {
        null,
        null
    };
    Vector2 oldTouchVector;
    float oldTouchDistance;
    float CameraSpeed = 4f;


    // Start is called before the first frame update
    void Start()
     {
         CurrentPosX = 0.5f;
         CurrentPosY = 0.0f;
         VectorBetweenPointsX = RightPoint.transform.position - LeftPoint.transform.position;
         VectorBetweenPointsY = UpPoint.transform.position - DownPoint.transform.position;

         myObjectPositioner = gameObject.GetComponent<ObjectPositioner>();
         //m_Raycaster = GetComponent<GraphicRaycaster>();
         //Debug.Log(VectorBetweenPointsX);
         //Debug.Log(VectorBetweenPointsY);
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
         if (onUI) return;
         if (inConstructionMode && myObjectPositioner.batiments != null && Input.GetKeyDown(KeyCode.Mouse0))
         {
            if (theConstructionWindow.SelectedBuild != null) myObjectPositioner.PlaceCubeNear(hitpoint, theConstructionWindow.SelectedBuild.GetComponent<BuildingIcon>().Prefab);
            else Debug.Log("Pas de batiment sélectionné");
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
     }

     //test click pour le téléphone
     void TestTouch()
     {
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

            if (inConstructionMode) myObjectPositioner.PlaceCubeNear(posOfHit, myObjectPositioner.batiments);
            else if((ObjectUnderMouse && ObjectUnderMouse.tag == "Swimmer")) ShowSwimmerStat(ObjectUnderMouse);
            else if (oldTouchPositions[0] == null || oldTouchPositions[1] != null)
            {
                oldTouchPositions[0] = Input.GetTouch(0).position;
                oldTouchPositions[1] = null;
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

     }
   
    //Test sur le déplacement de la caméra sur l'ordinateur
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
        SwimmerWindow.SetActive(true);
        SwimmerWindow.GetComponent<SwimmerWindow>().swimmer = swimmer;
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
        float calculForMusic = (transform.position.z - DownPoint.transform.position.z) / (UpPoint.transform.position.z - DownPoint.transform.position.z);
        //Debug.Log(calculForMusic);
        if (Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.WindowsEditor)
        {
            TestTouch();
            // Debug.Log(CurrentPosY);
            CurrentPosY = calculForMusic + 0.2f;
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
            MoveCamera();
        }
        BeachSound.volume = CurrentPosY - 0.2f;
        GetComponent<AudioSource>().volume = 1 - CurrentPosY - 0.30f;
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
}

