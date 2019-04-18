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

    private HitBox hitbox;

    public void setText(string text)
    {
        textChild.text = text;
    }

    public void setHitbox(HitBox hitbox)
    {
        this.hitbox = hitbox;
    }

    /*
    reimplement but for hitboxes
    public void onClick()
    {
        menuScreen.focusGesture(gesture);
    }
    */

}
