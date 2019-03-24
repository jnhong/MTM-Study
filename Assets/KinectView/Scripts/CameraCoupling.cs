using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCoupling : MonoBehaviour
{

    Camera m_MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = m_MainCamera.transform.position;
    }
}
