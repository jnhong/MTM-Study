using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitControls : MonoBehaviour
{
    [SerializeField]
    private GameObject center;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float angVel;
    [SerializeField]
    private float initialPitchAngle;
    [SerializeField]
    private float scrollSpeed;
    [SerializeField]
    private float minZoom;
    [SerializeField]
    private float maxZoom;

    // Start is called before the first frame update
    void Start()
    {

        // initialize position 
        if (center != null)
        {
            setInitialPosition();
        }

    }

    private void setInitialPosition()
    {
        Vector3 diff = new Vector3(0, 0, -distance);
        gameObject.transform.position = center.transform.position + diff;
        gameObject.transform.LookAt(center.transform);
        //rotateUp();
    }
    
    private void rotateUp()
    {
        Vector3 relPos = gameObject.transform.position - center.transform.position;
        if (Vector3.Angle(relPos, Vector3.up) > 20)
        {
            Vector3 rotAxis = Vector3.Cross(relPos, Vector3.up);
            transform.RotateAround(center.transform.position, rotAxis, initialPitchAngle);
        }
        if (center != null)
        {
            gameObject.transform.LookAt(center.transform);
        }
    }

    private void updateZoom()
    {
        float zoomFactor = - Input.mouseScrollDelta.y * scrollSpeed;
        Vector3 diff = gameObject.transform.position - center.transform.position;
        diff += diff * zoomFactor;
        if (diff.magnitude > minZoom && diff.magnitude < maxZoom)
        {
            gameObject.transform.position = center.transform.position + diff;
            gameObject.transform.LookAt(center.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateZoom();

        // move camera
        if (Input.GetKey(KeyCode.D)) // right
        {
            transform.RotateAround(center.transform.position, Vector3.up, - angVel * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) // left
        {
            transform.RotateAround(center.transform.position, Vector3.up, angVel * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) // down
        {
            Vector3 relPos = gameObject.transform.position - center.transform.position;
            if (Vector3.Angle(relPos, Vector3.up) < 160)
            {
                Vector3 rotAxis = Vector3.Cross(relPos, Vector3.up);
                transform.RotateAround(center.transform.position, rotAxis, - angVel * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.W)) // up
        {
            Vector3 relPos = gameObject.transform.position - center.transform.position;
            if (Vector3.Angle(relPos, Vector3.up) > 20)
            {
                Vector3 rotAxis = Vector3.Cross(relPos, Vector3.up);
                transform.RotateAround(center.transform.position, rotAxis, angVel * Time.deltaTime);
            }
        }

        /*
        if (Input.GetKey(KeyCode.Space))
        {
            setInitialPosition();
        }
        */


        // look at center object
        if (center != null)
        {
            gameObject.transform.LookAt(center.transform);
        }
        
    }
}
