using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollList : MonoBehaviour
{
    [SerializeField]
    private GestureManager gestureManager;
    [SerializeField]
    private GameObject buttonTemplate;

    public void populateList()
    {
        for (int i = 0; i < 20; ++i)
        {
            GameObject button = Instantiate(buttonTemplate);
            button.SetActive(true);

            button.GetComponent<ScrollListButton>().setText("Button #" + i);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

    public void initialize()
    {
        populateList();
    }

}
