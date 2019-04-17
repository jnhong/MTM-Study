using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;
    [SerializeField]
    private ScrollList scrollList;

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

    public void initialize()
    {
        scrollList.initialize();
    }
}
