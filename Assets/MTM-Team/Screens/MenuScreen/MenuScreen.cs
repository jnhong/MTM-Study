using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;
    [SerializeField]
    private ScrollList scrollList;

    private Gesture gesture; // gesture of focus

    public void setGesture(Gesture gesture)
    {
        this.gesture = gesture;
    }

    public void onGestureScreenButton()
    {
        gameObject.SetActive(false);
        stateManager.toGestureScreen();
        uninitialize();
    }

    public void onRecordScreenButton()
    {
        gameObject.SetActive(false);
        stateManager.toRecordScreen();
        uninitialize();
    }

    public void initialize()
    {
        scrollList.initialize();
    }

    public void uninitialize()
    {
        if (this.gesture != null)
        {
            this.gesture.disableGesture();
        }
    }

    public void focusGesture(Gesture gesture)
    {
        if (this.gesture != null)
        {
            this.gesture.disableGesture();
        }
    
        this.gesture = gesture;
        gesture.enableGesture();
    }
}
