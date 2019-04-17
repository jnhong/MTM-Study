using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureScrollListButton : MonoBehaviour
{
    [SerializeField]
    private MenuScreen menuScreen;
    [SerializeField]
    private GestureScrollList scrollList;
    [SerializeField]
    private Text textChild;

    private Gesture gesture;

    public void setText(string text)
    {
        textChild.text = text;
    }

    public void setGesture(Gesture gesture)
    {
        this.gesture = gesture;
        setText(gesture.getLabel());
    }

    public void onClick()
    {
        menuScreen.focusGesture(gesture);
    }




}
