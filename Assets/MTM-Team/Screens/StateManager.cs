﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    
    enum Screen
    {
        Menu = 0,
        Gesture,
        Record
    }

    Screen screen = Screen.Menu;

    [SerializeField]
    private MenuScreen menuScreen; 
    [SerializeField]
    private GestureScreen gestureScreen;
    [SerializeField]
    private RecordScreen recordScreen;


    // Start is called before the first frame update
    void Start()
    {
        menuScreen.gameObject.SetActive(false);
        gestureScreen.gameObject.SetActive(false);
        recordScreen.gameObject.SetActive(false);

        toMenuScreen();
    }

    /*
     * The following transition method assumes each screen
     * has closed their corresponding screen before calling.
     */

    public void toMenuScreen()
    {
        screen = Screen.Menu;
        menuScreen.gameObject.SetActive(true);
        menuScreen.initialize();
    }

    public void toGestureScreen()
    {
        screen = Screen.Gesture;
        gestureScreen.gameObject.SetActive(true);
        gestureScreen.initialize();
    }

    public void toRecordScreen()
    {
        screen = Screen.Record;
        recordScreen.gameObject.SetActive(true);
    }

}
