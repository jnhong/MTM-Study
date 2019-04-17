using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollListButton : MonoBehaviour
{
    [SerializeField]
    private MenuScreen menuScreen;
    [SerializeField]
    private ScrollList scrollList;
    [SerializeField]
    private Text textChild;

    private Gesture gesture;

    public void setText(string text)
    {
        textChild.text = text;
    }

    public void setGesture(Gesture gesture)
    {
        Debug.Log("scroll list button set gesture");
        this.gesture = gesture;
        setText(gesture.getLabel());
    }

    public void onClick()
    {
        Debug.Log("scroll list button click");
        menuScreen.focusGesture(gesture);
    }




}
