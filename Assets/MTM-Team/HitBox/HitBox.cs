using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    Gesture gesture;
    Renderer rend;

    List<string> joints;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = new Color(0, 1, 0, 0.25f);
    }

    public void setGesture(Gesture gesture)
    {
        this.gesture = gesture;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HandLeft")
        {
            rend.material.color = new Color(1, 0, 0, 0.25f);
            gesture.hit(gameObject);
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HandLeft")
        {
            rend.material.color = new Color(0, 1, 0, 0.25f);
        }
    }
    
}
