using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    public StateManager stateManager;

    public void onGestureScreenButton()
    {
        gameObject.SetActive(false);
        stateManager.toGestureScreen();
    }

    public void onRecordScreenButton()
    {
        gameObject.SetActive(false);
        stateManager.toRecordScreen();
    }
}
