using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitBoxPlacementControls : MonoBehaviour
{
    public GameObject hitBoxPreFab;
    public GameObject planeObject;
    public float scrollSpeed = 10.0f;
    public GestureManager gestureManager;

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
        gesture.addHitBox(newHitBox);
        // DRAW line
    }

    private void onEnter()
    {
        Debug.Log("on enter");
        gestureManager.addGesture(gesture);
        gesture.disableGesture();
        gesture = new Gesture();
    }

    private void onEscape()
    {
        uninitialize();
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
        gesture.deleteGesture();
        gesture = null;
    }
}

