using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DemoScreen : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;

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
        useCode("test");
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
