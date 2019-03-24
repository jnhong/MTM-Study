using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    Renderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = new Color(0, 1, 0, 0.25f);
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HandLeft")
        {
            rend.material.color = new Color(1, 0, 0, 0.25f);
            Debug.Log("OnTriggerEnter\n");
        }

    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HandLeft")
        {
            rend.material.color = new Color(0, 1, 0, 0.25f);
            Debug.Log("OnTriggerExit\n");
        }
    }
    
}
