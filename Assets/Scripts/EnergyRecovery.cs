using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRecovery : MonoBehaviour
{
    [SerializeField]
    private float _checkpointRadius;
    [SerializeField]
    private LayerMask _playerMask;
    private Energy _energyBar;

    private void Start()
    {
        _energyBar = FindObjectOfType<Energy>();
    }
    void Update()
    {
        if (IsPlayerTouch())
        {
            _energyBar.RestoreEnergy();
        }
        
    }
    private bool IsPlayerTouch()
    {
        return Physics2D.OverlapCircle(transform.position, _checkpointRadius, _playerMask);
    }

}
