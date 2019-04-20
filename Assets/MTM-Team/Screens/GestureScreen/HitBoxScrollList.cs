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
        LinkedList<GameObject> hitBoxes = gesture.getHitBoxes();

        int i = 0;
        foreach (GameObject hitBox in hitBoxes)
        {
            GameObject button = Instantiate(buttonTemplate);

            button.GetComponent<HitBoxScrollListButton>().setText("Hitbox " + i);
            button.GetComponent<HitBoxScrollListButton>().setHitBox(hitBox);
            
            button.SetActive(true);

            button.transform.SetParent(content.transform, false);

            ++i;
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

}
