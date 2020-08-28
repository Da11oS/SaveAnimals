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
        TerrainRenderar terrainRenderar;
        HeightMap heightMap = new HeightMap(target.Width);
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate"))
        {
            heightMap = target.Generate();
        }

        if (GUILayout.Button("Render"))
        {
            terrainRenderar = new TerrainRenderar(target.GroundTile, target.Tilemap);
            terrainRenderar.Render(heightMap);
        }
        if (GUILayout.Button("Clear"))
        {
            target.ClearTerraine(heightMap);
        }
    }
}
