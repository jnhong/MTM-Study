using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class CustomInputField : MonoBehaviour, ISelectHandler, IDeselectHandler 
{
    [SerializeField]
    private StateManager stateManager;

    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        stateManager.disableControls();
    }

    public void OnDeselect(BaseEventData data)
    {
        stateManager.enableControls();
    }

    public void OnDeselect()
    {
        stateManager.enableControls();
    }
}