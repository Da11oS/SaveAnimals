using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public LevelData Data;
    [Header(" ")]
    public LevelStart Begin;
    public LevelFinish End;
    public Border Border;
    public EnergyPoint[] EnergyPoints;
    public void Awake()
    {
        Border = GetComponentInChildren<Border>();
        Begin = GetComponentInChildren<LevelStart>();
        End = GetComponentInChildren<LevelFinish>();
        EnergyPoints = GetComponentsInChildren<EnergyPoint>(); 
    }
    public void Start()
    {
        Data.CheckOnWork = "Is working;";
    }
    public void SetActive(bool isActive)
    {
        Data.IsActive = isActive;
        if (isActive)
        {
            Location.Instance.Data.OpenCount++;
        }
        else Location.Instance.Data.OpenCount--;
        gameObject.SetActive(isActive);
    }
}
