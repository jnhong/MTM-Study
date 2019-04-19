using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    Gesture gesture;
    Renderer rend;

    List<string> joints;

    Color green;
    Color yellow;
    Color red;

    Color inactiveColor;
    
    void Awake()
    {
        initialize();
    }

    private void initialize()
    {
        green = new Color(0, 1, 0, 0.25f);
        yellow = new Color(1, 1, 0, 0.25f);
        red = new Color(1, 0, 0, 0.25f);
        rend = GetComponent<Renderer>();
        inactiveColor = green;
        rend.material.color = inactiveColor;
    }

    public void highlight()
    {
        inactiveColor = yellow;
        rend.material.color = inactiveColor;
    }

    public void unhighlight()
    {
        inactiveColor = green;
        rend.material.color = inactiveColor;
    }

    public void setGesture(Gesture gesture)
    {
        this.gesture = gesture;
    }

    public void refreshLine()
    {
        gesture.refreshLine();
    }

    public void setGrey()
    {
        rend.material.color = new Color(0.5f, 0.5f, 0.5f, 0.25f);
    }

    public void setGreen()
    {
        rend.material.color = green;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HandLeft")
        {
            rend.material.color = red;
            if (gesture != null)
            {
                gesture.hit(gameObject);
            }
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HandLeft")
        {
            rend.material.color = inactiveColor;
        }
    }
    
}
