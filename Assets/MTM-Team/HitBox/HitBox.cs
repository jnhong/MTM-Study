using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    Gesture gesture;
    Renderer rend;

    List<string> joints;
    int numInside;

    Color green;
    Color yellow;
    Color red;

    bool highlighted;

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
        rend.material.color = green;
        numInside = 0;
        highlighted = false;
        if (joints == null)
        {
            joints = new List<string>();
        }
    }

    public void addJoint(string joint)
    {
        joints.Add(joint);
    }

    public bool hasJoint(string joint)
    {
        return joints.Contains(joint);
    }
    public void clearJoints()
    {
        joints.Clear();
    }

    public void highlight()
    {
        highlighted = true;
        if (numInside == 0)
        {
            rend.material.color = yellow;
        } 
    }

    public void unhighlight()
    {
        highlighted = false;
        if (numInside == 0)
        {
            rend.material.color = green;
        } 
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
        if (joints.Contains(other.gameObject.name))
        {
            if (numInside == 0)
            {
                rend.material.color = red;
            }
            ++numInside;
            if (gesture != null)
            {
                gesture.hit(gameObject);
            }
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (joints.Contains(other.gameObject.name))
        {
            --numInside;
            if (numInside == 0)
            {
                rend.material.color = highlighted ? yellow : green;
            }
        }
    }
    
}
