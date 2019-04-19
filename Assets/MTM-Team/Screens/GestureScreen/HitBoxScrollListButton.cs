using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBoxScrollListButton : MonoBehaviour
{
    [SerializeField]
    private GestureScreen gestureScreen;
    [SerializeField]
    private HitBoxScrollList hitBoxScrollList;
    [SerializeField]
    private Text textChild;

    private GameObject hitBox;

    public void setText(string text)
    {
        textChild.text = text;
    }

    public void setHitBox(GameObject hitBox)
    {
        this.hitBox = hitBox;
    }

    public void onClick()
    {
        gestureScreen.focusHitBox(hitBox, textChild.text);
    }

}
