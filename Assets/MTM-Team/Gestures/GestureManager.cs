using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    List<Gesture> gestures;

    private void Start()
    {
        gestures = new List<Gesture>();
    }
    
    public void addGesture(Gesture gesture)
    {
        gestures.Add(gesture);
    }

    public void removeGesture(Gesture gesture)
    {
        gestures.Remove(gesture);
    }

    public List<Gesture> gesturesList()
    {
        return gestures;
    } 

    public void beginRecording()
    {
        foreach (Gesture gesture in gestures)
        {
            //Debug.Log("begin recording: " + gesture.label);
            gesture.beginRecording();
        }
    }

    public void endRecording()
    {
        foreach (Gesture gesture in gestures)
        {
            //Debug.Log("end recording: " + gesture.label);
            gesture.endRecording();
        }
    }
    

}
