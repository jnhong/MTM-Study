using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordScreen : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;
    [SerializeField]
    private GestureManager gestureManager;
    [SerializeField]
    private FileWriter fileWriter;

    public void onMenuScreenButton()
    {
        gameObject.SetActive(false);
        uninitialize();
        stateManager.toMenuScreen();
    }

    public void initialize()
    {
        gestureManager.beginRecording();
    }

    public void uninitialize()
    {
        gestureManager.endRecording();
    }
}
