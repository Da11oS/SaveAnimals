using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public static Levels Instance;
    public static Level _currentLevel;

    [SerializeField]
    private List<Level> _levels;
    private Player _player;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        if(_levels == null)
            SetLevels();
    }
    void Start()
    {
        for (int i = _levels.Count - 1; i >= 0; i--)
        {
            if(_levels[i].IsOpen)
            {
                OpenLevel(_levels[i]);
                break;
            }
        }
    }

    public void OpenLevel(int id)
    {
        foreach(var level in _levels)
        {
            if(level.Id == id)
            {
                OpenLevel(level);
                break;
            }
        }
    }
    public void OpenLevel(Level level)
    {

        foreach (var child in _levels)
        {
            if (child == level)
            {
                child.SetActive(true);
                _currentLevel = child;
            }
            else child.SetActive(false);

        }
    }
    public void SetLevels()
    {
        _levels = new List<Level>(FindObjectsOfType<Level>());

    }
    public void SetLevelIsOpen(int id)
    {
        foreach (var level in _levels)
        {
            if (level.Id == id)
            {
                level.IsOpen = true;
                break;
            }
        }
    }
    public void SetAll(bool isActive)
    {
        foreach (var level in _levels)
        {
            level.SetActive(isActive);
        }
    }
}
