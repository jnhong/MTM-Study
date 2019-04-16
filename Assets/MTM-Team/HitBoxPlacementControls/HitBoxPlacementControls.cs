using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxPlacementControls : MonoBehaviour
{

    Vector3 initialPosition;
    public GameObject hitBoxPreFab;
    public GameObject planeObject;
    public float scrollSpeed = 10.0f;

    enum Mode
    {
        PLANE,
        VERTICAL
    }
    Mode mode;
    Plane plane;

    // Use list of hitboxes later
    GameObject hitBox; // box to be place

 

    // Start is called before the first frame update
    void Start()
    {
        mode = Mode.PLANE;
        initialPosition = gameObject.transform.position;
        // remove later
        initialize();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            initialize();
        }

        // get ray from camera
        // intersect with plane
        // move box to new position on plane

        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float enter = 0.0f;

        //if (mode == Mode.PLANE) {
            if (plane.Raycast(ray, out enter))
            {
                //Get the point that is clicked
                Vector3 hitPoint = ray.GetPoint(enter);

                //Move your cube GameObject to the point where you clicked
                hitBox.transform.position = hitPoint;
            }
        //} else if (mode == Mode.VERTICAL) {
            Vector3 offset = new Vector3(0, Input.mouseScrollDelta.y * scrollSpeed, 0);
            plane = Plane.Translate(plane, - offset);
            planeObject.transform.position += offset;
            hitBox.transform.position += offset;
        //}

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("hello");
            mode = (mode == Mode.PLANE) ? Mode.VERTICAL : Mode.PLANE;
        }
        */

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(hitBox);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            planeObject.SetActive(false);
        }


        
    }

    // call back externally to begin box placement
    public void initialize()
    {
        planeObject.transform.position = initialPosition;
        planeObject.transform.localScale = new Vector3(10, 10, 10);
        planeObject.SetActive(true);
        plane = new Plane(planeObject.transform.up, planeObject.transform.position);
        hitBox = Instantiate(hitBoxPreFab, initialPosition, Quaternion.identity);
    }

    public void uninitialize()
    {
        planeObject.SetActive(false);
    }
}
