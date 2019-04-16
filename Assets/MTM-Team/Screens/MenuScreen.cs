using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    public StateManager stateManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
