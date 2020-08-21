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
        TerrainRenderar TerrainRenderar;
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate"))
        {
            target.Generate();
            TerrainRenderar = new TerrainRenderar(target.HeightMap, target.GroundTile, target.Tilemap);
        }

        if (GUILayout.Button("Render"))
        {
            TerrainRenderar = new TerrainRenderar(target.HeightMap, target.GroundTile, target.Tilemap);
            TerrainRenderar.Render();
        }
        if (GUILayout.Button("Clear"))
        {
            target.ClearTerraine();
        }
    }
}
