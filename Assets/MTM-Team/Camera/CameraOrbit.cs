using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public GameObject center;
    public float distance;
    public float angVel;

    // Start is called before the first frame update
    void Start()
    {
        angVel = 100.0f;
        distance = 60.0f;

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
    }

    // Update is called once per frame
    void Update()
    {
        // move camera
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(center.transform.position, Vector3.up, - angVel * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(center.transform.position, Vector3.up, angVel * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 relPos = gameObject.transform.position - center.transform.position;
            if (Vector3.Angle(relPos, Vector3.up) < 160)
            {
                Vector3 rotAxis = Vector3.Cross(relPos, Vector3.up);
                transform.RotateAround(center.transform.position, rotAxis, - angVel * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 relPos = gameObject.transform.position - center.transform.position;
            if (Vector3.Angle(relPos, Vector3.up) > 20)
            {
                Vector3 rotAxis = Vector3.Cross(relPos, Vector3.up);
                transform.RotateAround(center.transform.position, rotAxis, angVel * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            setInitialPosition();
        }


        // look at center object
        if (center != null)
        {
            gameObject.transform.LookAt(center.transform);
        }
        
    }
}
