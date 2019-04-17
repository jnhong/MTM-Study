﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitBoxPlacementControls : MonoBehaviour
{
    [SerializeField]
    private GameObject hitBoxPreFab;
    [SerializeField]
    private GameObject planeObject;
    [SerializeField]
    private float scrollSpeed = 10.0f;
    [SerializeField]
    private GestureManager gestureManager;
    [SerializeField]
    private GestureScreen gestureScreen;

    Plane plane;
    GameObject hitBox; // box to be place
    Gesture gesture; // sequence of hitboxes

    // Update is called once per frame
    void Update()
    {
        // move the hitbox under the mouse
        updateMouseHitBox();

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            onClick();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onEscape();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            onEnter();
        }
    }

    private void updateMouseHitBox()
    {
        // get ray from camera
        // intersect with plane
        // move box to new position on plane

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float enter = 0.0f;

        if (plane.Raycast(ray, out enter))
        {
            //Get the point that is clicked
            Vector3 hitPoint = ray.GetPoint(enter);

            //Move your cube GameObject to the point where you clicked
            hitBox.transform.position = hitPoint;
        }
        Vector3 offset = new Vector3(0, Input.mouseScrollDelta.y * scrollSpeed, 0);
        plane = Plane.Translate(plane, -offset);
        planeObject.transform.position += offset;
        hitBox.transform.position += offset;
    }

    private void onClick()
    {
        GameObject newHitBox = Instantiate(hitBox);
        hitBox.SetActive(true);
        gesture.addHitBox(newHitBox);
        // DRAW line
    }

    // submit new gesture to gesture manager then exit to menu screen
    private void onEnter()
    {
        gestureManager.addGesture(gesture);
        gesture.disableGesture();
        gesture = null;
        gestureScreen.onMenuScreenButton();
    }

    // exit to menu screen
    private void onEscape()
    {
        gestureScreen.onMenuScreenButton();
    }

    // call to begin gesture creation
    public void initialize()
    {
        Vector3 initialPosition = gameObject.transform.position;
        planeObject.transform.position = initialPosition;
        planeObject.transform.localScale = new Vector3(10, 10, 10);
        planeObject.SetActive(true);
        plane = new Plane(planeObject.transform.up, planeObject.transform.position);
        if (!hitBox)
        {
            hitBox = Instantiate(hitBoxPreFab, initialPosition, Quaternion.identity);
        } 
        hitBox.SetActive(true);
        gesture = new Gesture();
    }

    // call to exit gesture creation
    public void uninitialize()
    {
        planeObject.SetActive(false);
        hitBox.SetActive(false);
        if (gesture != null)
        {
            gesture.deleteGesture();
            gesture = null;
        }
    }
}

