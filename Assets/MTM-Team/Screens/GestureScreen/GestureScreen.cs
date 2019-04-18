using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureScreen : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;
    [SerializeField]
    private HitBoxPlacementControls controls;
    [SerializeField]
    private GestureManager gestureManager;
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private HitBoxScrollList hitBoxScrollList;
    [SerializeField]
    private DemoScreen demoScreen;

    private Gesture gesture;
    
    public Gesture getGesture()
    {
        return gesture;
    }

    public void initialize()
    {
        controls.gameObject.SetActive(true);
        controls.initialize();
        gesture = new Gesture();
        inputField.text = "unlabeled";
    }

    private void uninitialize()
    {
        controls.gameObject.SetActive(false);
        controls.uninitialize();
        if (gesture != null)
        {
            gesture.deleteGesture();
            gesture = null;
        }
        hitBoxScrollList.clearList();
        demoScreen.uninitialize();
    }

    public void addHitBox(GameObject newHitBox)
    {
        gesture.addHitBox(newHitBox);
        hitBoxScrollList.refresh();
    }

    public void onDemoButton()
    {
        demoScreen.initialize();
    }

    public void onMenuScreenButton()
    {
        gameObject.SetActive(false);
        stateManager.toMenuScreen();
        uninitialize();
    }
    
    public void onSubmitButton()
    {
        submitGesture();
        onMenuScreenButton();
    }

    public void submitGesture()
    {
        gesture.label = inputField.text;
        gestureManager.addGesture(gesture);
        gesture.disableGesture();
        gesture = null;
    }

}
