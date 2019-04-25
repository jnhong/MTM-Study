using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DemoScreen : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private Dropdown distanceDropdown;
    [SerializeField]
    private Dropdown reinforcementDropdown;
    [SerializeField]
    private Dropdown firstDropdown;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void initialize()
    {
        gameObject.SetActive(true);
    }

    public void uninitialize()
    {
        gameObject.SetActive(false);
    }

    public void onApplyButton()
    {
        string code = "";
        if (distanceDropdown.value == 0)
        {
            if (reinforcementDropdown.value == 0)
            {
                code += "CZ-IBAX";
            } else
            {
                code += "CZ-IBBX";
            }
        } else
        {
            if (reinforcementDropdown.value == 0)
            {
                code += "CZ-IBCX";
            }
            else
            {
                code += "CZ-IBDX";
            }
        }
        if (firstDropdown.value == 0)
        {
            code += "E";
        } else
        {
            code += "Z";
        }
        useCode(code);
        uninitialize();
    }

    public void useCode(string code)
    {
        inputField.text = code;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
