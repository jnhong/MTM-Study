using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureScreen : MonoBehaviour
{
    public StateManager stateManager;

    public void onMenuScreenButton()
    {
        gameObject.SetActive(false);
        stateManager.toMenuScreen();
    }
}
