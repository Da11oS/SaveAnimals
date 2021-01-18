using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Levels))]
public class LevelsInspector : Editor
{
    public Levels Target;
    int _level;
    public void OnEnable()
    {
        Target = (Levels)base.target;
    }

    public override void OnInspectorGUI()
    {
        
        if(GUILayout.Button("Set levels"))
        {
            Target.SetLevels();
        }
        _level = EditorGUILayout.IntField("Level", _level);
        if (GUILayout.Button("Open level"))
        {
            Target.OpenLevel(_level);
        }
        if (GUILayout.Button("Set all open"))
        {
            Target.SetAll(true);
        }
        if (GUILayout.Button("Set all close"))
        {
            Target.SetAll(false);
        }
        if (GUILayout.Button("Open level"))
        {
            Target.OpenLevel(_level);
        }
        base.OnInspectorGUI();
    }
}
