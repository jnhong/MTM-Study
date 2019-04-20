﻿using System.Collections;
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
    private Text currentHitBoxText;
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private HitBoxScrollList hitBoxScrollList;
    [SerializeField]
    private GameObject jointToggles;

    private Gesture gesture;
    private GameObject hitBox;
    
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
    }

    public void addHitBox(GameObject newHitBox)
    {
        setJointToggles(newHitBox);
        gesture.addHitBox(newHitBox);
        hitBoxScrollList.refresh();
    }

    public void setJointToggles(GameObject newHitBox)
    {
        HitBox h = newHitBox.GetComponent<HitBox>();
        h.clearJoints();
        foreach (Transform child in jointToggles.transform)
        {
            if (child.GetComponent<Toggle>().isOn)
            {
                h.addJoint(child.Find("Label").GetComponent<Text>().text);
            }
        }
    }

    public void onMenuScreenButton()
    {
        gameObject.SetActive(false);
        uninitialize();
        stateManager.toMenuScreen();
    }
    
    public void onSubmitButton()
    {
        submitGesture();
        onMenuScreenButton();
    }

    public void onDeleteButton()
    {
        if (hitBox != null)
        {
            gesture.removeHitBox(hitBox);
            controls.toOff();
            hitBoxScrollList.refresh();
        }
    }

    public void submitGesture()
    {
        gesture.label = inputField.text;
        gestureManager.addGesture(gesture);
        gesture.disableGesture();
        gesture = null;
    }

    public void focusHitBox(GameObject hitBox, string hitBoxName)
    {
        this.hitBox = hitBox;
        setUIToggles();
        controls.beginMovement(hitBox, hitBoxName);
    }

    public void unfocusHitBox()
    {
        hitBox = null;
    }

    private void setUIToggles()
    {
        HitBox h = hitBox.GetComponent<HitBox>();
        foreach (Transform child in jointToggles.transform)
        {
            // get child joint string
            // check if in hitbox
            // set toggle is so
            string joint = child.Find("Label").GetComponent<Text>().text;
            if (h.hasJoint(joint))
            {
                child.GetComponent<Toggle>().isOn = true;
            } else
            {
                child.GetComponent<Toggle>().isOn = false;
            }
        }
    }

    public void setCurrentHitBoxText(string s)
    {
        currentHitBoxText.text = s;
    }


}
