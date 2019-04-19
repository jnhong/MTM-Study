using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHitBox : MonoBehaviour
{

    enum Mode
    {
        Begin,
        End
    }

    Mode mode;

    [SerializeField]
    Text text;
    [SerializeField]
    HitBoxPlacementControls controls;

    // Start is called before the first frame update
    void Start()
    {
        mode = Mode.Begin;
        text.text = "Add";
    }

    public void onClick()
    {
        switch (mode)
        {
            case Mode.Begin:
                onBegin();
                break;
            case Mode.End:
                onEnd();
                break;
            default:
                break; ;
        }
    }

    private void onBegin()
    {
        controls.beginPlacement();
        mode = Mode.End;
        text.text = "End";
    }

    private void onEnd()
    {
        controls.endPlacement();
        // onEndState called by controls
    }

    public void onEndState()
    {
        text.text = "Add";
        mode = Mode.Begin;
    }

}
