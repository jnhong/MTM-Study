using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureScrollList : MonoBehaviour
{
    [SerializeField]
    private GestureManager gestureManager;
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GameObject content;

    public void populateList()
    {
        List<Gesture> gestures = gestureManager.gesturesList();

        for (int i = 0; i < gestures.Count; ++i)
        {
            Gesture gesture = gestures[i];
            GameObject button = Instantiate(buttonTemplate);

            button.GetComponent<GestureScrollListButton>().setGesture(gesture);
            
            button.SetActive(true);

            button.transform.SetParent(content.transform, false);
        }
    }

    public void clearList()
    {
        // destroy all children except first (which is the button template)
        for (int i = content.transform.childCount - 1; i > 0; --i)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }
    
    public void refresh()
    {
        clearList();
        populateList();
    }

    public void initialize()
    {
        refresh();
    }

    public void removeGesture(Gesture gesture)
    {
        gestureManager.removeGesture(gesture);
        refresh();
    }

}
