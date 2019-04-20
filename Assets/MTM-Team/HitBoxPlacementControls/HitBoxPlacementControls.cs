using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HitBoxPlacementControls : MonoBehaviour
{
    [SerializeField]
    private GameObject hitBoxPreFab;
    [SerializeField]
    private GameObject planeObject;
    [SerializeField]
    private float planeSpeed = 10.0f;
    [SerializeField]
    private GestureScreen gestureScreen;
    [SerializeField]
    private AddHitBox addButton;
    
    enum Mode 
    {
        Off, // neither
        Move, // move a pre-existing box
        Place // place a new box
    }

    Mode mode;

    Plane plane;
    GameObject mouseHitBox; // box to be place
    GameObject moveHitBox; // box to be moved 
    bool setMove;
    GameObject oldHitBox;

    // Update is called once per frame
    void Update()
    {
        // move the hitbox under the mouse
        switch (mode)
        {
            case Mode.Off:
                break;
            case Mode.Move:
                updateMoveHitBox();
                break;
            case Mode.Place:
                updateMouseHitBox();
                break;
            default:
                Debug.Log("error: hit box controls in unknown state");
                break;
        }

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

        if (Input.GetKey(KeyCode.Q)) // move plane down 
        {
            updatePlane(Time.deltaTime * - planeSpeed);
        }

        if (Input.GetKey(KeyCode.E)) // move plane up 
        {
            updatePlane(Time.deltaTime * planeSpeed);
        }
    }

    private void updatePlane(float deltaY)
    {
        Vector3 offset = new Vector3(0, deltaY, 0);
        plane = Plane.Translate(plane, -offset);
        planeObject.transform.position += offset;
        mouseHitBox.transform.position += offset;
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
            mouseHitBox.transform.position = hitPoint;
        }
    }

    private void updateMoveHitBox()
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
            moveHitBox.transform.position = hitPoint;
            moveHitBox.GetComponent<HitBox>().refreshLine();
        }
    }

    private void onClick()
    {
        switch (mode)
        {
            case Mode.Off:
                break;
            case Mode.Move:
                onClickMovement();
                break;
            case Mode.Place:
                onClickPlacement();
                break;
            default:
                Debug.Log("error: hit box controls in unknown state");
                break;
        }
    }

    private void onClickPlacement()
    {
        GameObject newHitBox = Instantiate(mouseHitBox);
        gestureScreen.addHitBox(newHitBox);
    }

    private void onClickMovement()
    {
        setMove = true;
        endMovement();
    }

    // submit new gesture to gesture manager then exit to menu screen
    private void onEnter()
    {
        gestureScreen.submitGesture();
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
        // set up plane and mode
        Vector3 initialPosition = gameObject.transform.position;
        planeObject.transform.position = initialPosition;
        planeObject.transform.localScale = new Vector3(10, 10, 10);
        planeObject.SetActive(true);
        plane = new Plane(planeObject.transform.up, planeObject.transform.position);
        mode = Mode.Off;
    }

    // call to exit gesture creation
    public void uninitialize()
    {
        planeObject.SetActive(false);
        switch (mode)
        {
            case Mode.Off:
                break;
            case Mode.Move:
                endMovement();
                break;
            case Mode.Place:
                endPlacement();
                break;
            default:
                Debug.Log("error: hit box controls in unknown state");
                break;
        }
    }

    public void beginPlacement()
    {
        toOff();
        // create hover box
        if (mouseHitBox == null)
        {
            Vector3 initialPosition = gameObject.transform.position;
            mouseHitBox = Instantiate(hitBoxPreFab, initialPosition, Quaternion.identity);
        } 
        mouseHitBox.SetActive(true);
        mode = Mode.Place;
    }

    public void endPlacement()
    {
        // hide hover box
        mouseHitBox.SetActive(false);
        mode = Mode.Off;
        addButton.onEndState();
    }

    public void beginMovement(GameObject moveHitBox)
    {
        toOff();
        // make copy to save original state 
        oldHitBox = Instantiate(moveHitBox);
        oldHitBox.GetComponent<HitBox>().setGrey();
        setMove = false;
        this.moveHitBox = moveHitBox;
        mode = Mode.Move;
    }

    public void endMovement()
    {
        if (!setMove)
        {
            // reset moveHitBox
            moveHitBox.transform.position = oldHitBox.transform.position;
            moveHitBox.transform.localScale = oldHitBox.transform.localScale;
        }
        Destroy(oldHitBox);
        oldHitBox = null;
        moveHitBox = null;
        mode = Mode.Off;
    }

    public void toOff()
    {
        switch (mode)
        {
            case Mode.Move:
                endMovement();
                break;
            case Mode.Place:
                endPlacement();
                break;
            default:
                break;
        }
    }

}

