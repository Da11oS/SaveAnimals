using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Location", menuName = "Levels/Location data", order = 2)]
public class LocationData : ScriptableObject
{
    public LevelData CurrentLevelData;
    public int OpenCount;
    public LevelData[] Levels;
    public LockStatus CurrentStatus;

}