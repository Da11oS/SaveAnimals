using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public Action<Level> OnChangeLevel;
    public LocationData Data;
    public static Location Instance;
    [SerializeField]
    private List<Level> _levels;
    public Level CurrentLevel;

    public void Start()
    {
        SetInstance();
        SetAllActive(false);
        OpenLevel(Data.CurrentLevelData.Id);
        OnEnable();
    
    }

    public void OpenLevel(int id)
    {
        Openlevel(GetLevelById(id));
    }
    public Level GetLevelById(int id)
    {
        foreach (var level in _levels)
        {
            if (level.Data.Id == id)
                return level;
        }
        return CurrentLevel;        
    }
    public void Openlevel(Level level)
    {
        if (level.Data.CurrentStatus is LockStatus.Unlocked)
        {
            Data.CurrentLevelData = level.Data;

            foreach(var child in _levels)
            {
                if(child.Data.Id == Data.CurrentLevelData.Id)
                    child.gameObject.SetActive(false);
            }
            CurrentLevel = level;
            OnChangeLevel?.Invoke(level);
            level.gameObject.SetActive(true);
        }
        else
        {
            print("is locked");
        }
    }
    public void SetLevelStatus(int id, LockStatus status)
    {
        foreach (var level in _levels)
        {
            if (level.Data.Id == id)
            {
                level.Data.CurrentStatus = status;
                break;
            }
        }
    }
    public void SetPlayerPosition(Level level)
    {
        Player.Instance.transform.position = level.Begin.transform.position;
    }
    public void SetCumeraBorders(Level level)
    {
        
        CustomCamera.Instance.Border = level.Border;
    }
    public void Restart()
    {
        SetPlayerPosition(CurrentLevel);
        foreach(var energy in CurrentLevel.EnergyPoints)
        {
            energy.gameObject.SetActive(true);
        }
    }
    private void OnEnable()
    {
        
        OnChangeLevel += SetPlayerPosition;
        OnChangeLevel += SetCumeraBorders;
    }
    private void OnDisable()
    {
        if (OnChangeLevel != null)
        {
            OnChangeLevel -= SetPlayerPosition;
            OnChangeLevel -= SetCumeraBorders;
        }
    }
    public void SetAllActive(bool isActive = true)
    {
        foreach (var level in _levels)
        {
            level.gameObject.SetActive(isActive);
        }
    }
    private void SetInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
