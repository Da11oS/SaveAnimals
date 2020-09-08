using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScrollMenu))]
public class ScrollMenuInspector : Editor
{
    public override void OnInspectorGUI()
    {
        ScrollMenu target = (ScrollMenu)base.target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Instantiate panels"))
        {
            target.SetPanels();
        }
        if (GUILayout.Button("Clear panels"))
        {
            target.ClearPanels();
        }
    }
}
