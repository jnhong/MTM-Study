using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollListButton : MonoBehaviour
{
    [SerializeField]
    private ScrollList scrollList;
    [SerializeField]
    private Text textChild;

    public void setText(string text)
    {
        textChild.text = text;
    }

    public void onClick()
    {
        // do something
    }




}
