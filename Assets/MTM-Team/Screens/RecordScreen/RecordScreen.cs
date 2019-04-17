using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordScreen : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;

    public void onMenuScreenButton()
    {
        gameObject.SetActive(false);
        stateManager.toMenuScreen();
    }
}
