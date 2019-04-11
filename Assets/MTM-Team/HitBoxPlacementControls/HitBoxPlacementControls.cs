using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxPlacementControls : MonoBehaviour
{

    bool active;
    Vector3 initialPosition;
    public GameObject hitBoxPreFab;
    public float scrollSpeed = 10.0f;

    enum Mode
    {
        PLANE,
        VERTICAL
    }
    Mode mode;
    Plane plane;
    GameObject planeObject;
    // Use list of hitboxes later
    GameObject hitBox; // box to be place

 

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        mode = Mode.PLANE;
        initialPosition = gameObject.transform.position;
        // remove later
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) {
            return;
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
        
    }

    // call back externally to begin box placement
    void initialize()
    {
        active = true;
        plane = new Plane(Vector3.up, initialPosition);
        planeObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
        planeObject.transform.position = initialPosition;
        planeObject.transform.localScale += new Vector3(10, 10, 10);
        hitBox = Instantiate(hitBoxPreFab, initialPosition, Quaternion.identity);
    }
}
