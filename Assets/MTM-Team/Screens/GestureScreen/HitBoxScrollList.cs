using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScrollList : MonoBehaviour
{
    [SerializeField]
    private GestureScreen gestureScreen;
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GameObject content;

    public void populateList()
    {
        Gesture gesture = gestureScreen.getGesture();

        for (int i = 0; i < gesture.getCount(); ++i)
        {
            GameObject button = Instantiate(buttonTemplate);

            button.GetComponent<HitBoxScrollListButton>().setText("Hitbox " + i);
            
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

    /*
    // reimplement but for hitboxes
    public void removeGesture(Gesture gesture)
    {
        gestureManager.removeGesture(gesture);
        refresh();
    }
    */
}
