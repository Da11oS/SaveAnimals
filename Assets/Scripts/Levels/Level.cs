using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    
    public bool IsOpen;
    public bool IsActive;
    public int Id;

    public LevelStart Begin { get => GetComponentInChildren<LevelStart>(); }
    public LevelFinish End { get => GetComponentInChildren<LevelFinish>(); }

    public LeftBottomBorder LeftBottomBorder { get => GetComponentInChildren<LeftBottomBorder>(); }
    public RightUpperBorder RightUpperBorder { get => GetComponentInChildren<RightUpperBorder>(); }

    public static int OpenCount;
    public Transform[] Childs;
    public void Awake()
    {
      
    }
    public void SetActive(bool isActive)
    {
        IsActive = isActive;
        Childs = gameObject.GetComponentsInChildren<Transform>();
        if (isActive)
            OpenCount++;
        else OpenCount--;
        
        for(int i = 0; i < Childs.Length;i++)
        {
            if(Childs[i] != transform)
                Childs[i].gameObject.SetActive(isActive);
        }
    }
}
