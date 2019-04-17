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

    public List<Gesture> gesturesList()
    {
        return gestures;
    } 

}
