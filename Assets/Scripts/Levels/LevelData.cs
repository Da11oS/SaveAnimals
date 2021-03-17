using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

[CreateAssetMenu(fileName = "LevelData", menuName = "Levels/Level data", order = 2)]
public class LevelData : ScriptableObject
{

    public bool IsActive;
    public int Id;
    public LockStatus CurrentStatus;
    public string CheckOnWork;
}
public enum LockStatus
{ Locked, Unlocked };
