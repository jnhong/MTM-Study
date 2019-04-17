using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;
    [SerializeField]
    private GestureScrollList scrollList;
    [SerializeField]
    private Text currentGestureText;

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
    
    public void onQuitButton()
    {
        Debug.Log("Application quit.");
        Application.Quit();
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
        currentGestureText.text = gesture.label;
    }
}
