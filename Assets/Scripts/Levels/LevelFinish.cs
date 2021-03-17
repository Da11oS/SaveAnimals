using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    Action OnChangeLevel;
    // Start is called before the first frame update
    private Level _parentLevel;
    private void Start()
    {
        _parentLevel = GetComponentInParent<Level>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Location.Instance.SetLevelStatus(_parentLevel.Data.Id + 1, LockStatus.Unlocked);
            Location.Instance.OpenLevel(_parentLevel.Data.Id + 1);
            Debug.Log(_parentLevel.Data.Id + 1 + " Is finish");
        }
    }
}
