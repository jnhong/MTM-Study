using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureScreen : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;
    [SerializeField]
    private HitBoxPlacementControls controls;

    public void initialize()
    {
        controls.gameObject.SetActive(true);
        controls.initialize();
    }

    private void uninitialize()
    {
        controls.gameObject.SetActive(false);
        controls.uninitialize();
    }

    public void onMenuScreenButton()
    {
        gameObject.SetActive(false);
        stateManager.toMenuScreen();
        uninitialize();
    }
    
    public void onSubmitButton()
    {
        controls.submitGesture();
        onMenuScreenButton();
    }
}
