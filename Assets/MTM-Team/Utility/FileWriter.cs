using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileWriter : MonoBehaviour
{
    public string path;
    public string filename;
    public Vector2 scrollPosition = Vector2.zero;
    private string log = "";

    // Start is called before the first frame update
    void Start()
    {
        if (!path.EndsWith("\\"))           
            path = path + "\\";
        File.WriteAllText(Path.Combine(path, filename), log);
    }

    public void WriteCode(string code)
    {
        log += code;
        File.AppendAllText(Path.Combine(path, filename), code + Environment.NewLine);
    }

    private void OnGUI()
    {
        scrollPosition = GUI.BeginScrollView(new Rect(Screen.width - 110, Screen.height - 110, 100, 100), scrollPosition, new Rect(0, 0, 80, 200));
        GUI.TextArea(new Rect(0, 0, 80, 200), log);
        GUI.EndScrollView();
    }
}
