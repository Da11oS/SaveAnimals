using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Terrain;

[CustomEditor(typeof(TerraineGenerator))]
public class TerraineGenratorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        TerraineGenerator target = (TerraineGenerator)base.target;
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate!"))
        {
            target.Generate();
        }
        if(GUILayout.Button("Render"))
        {
            TerrainRenderar TerrainRenderar = new TerrainRenderar(target.HeightMap, target.GroundTile, target.Tilemap);
            TerrainRenderar.Render();
        }
        if (GUILayout.Button("Clear"))
        {
            target.ClearTerraine();
        }
    }
}
