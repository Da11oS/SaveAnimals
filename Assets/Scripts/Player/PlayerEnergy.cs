using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEnergy : MonoBehaviour
{
    public event Action<float> OnEnergyChange;
    public static float StartEnergy { get => 1; }
    private float _energy;
    [Range(0, 1)] [SerializeField] private float _costRate;
    [SerializeField] private float _checkpointRadius;
    [SerializeField] private LayerMask _energyRecoveryPointMask;
    private Collider2D _checkpointCollider;
    void Start()
    {
        _energy = StartEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        _checkpointCollider = Physics2D.OverlapCircle(transform.position, _checkpointRadius, _energyRecoveryPointMask);
        if (_energy > 0)
        {
            ReduceEnergy(Time.deltaTime * _costRate);
        }
        if(IsCheckpoint())
        {

            _checkpointCollider.gameObject.SetActive(false);
            SetEnergy(StartEnergy);
        }
    }
    public void ReduceEnergy(float damage)
    {
        if (damage <= 1)
            _energy -= damage;
        if(_energy <= 0)
        {
            
            Location.Instance.Restart();
            _energy = StartEnergy;
        }
        if (OnEnergyChange != null)
        {
            OnEnergyChange.Invoke(_energy);
        }
    }

    private void SetEnergy(float value)
    {
        _energy = value;
        if (OnEnergyChange != null)
        {
            OnEnergyChange.Invoke(_energy);
        }
    }
    private bool IsCheckpoint()
    {

        return _checkpointCollider;
    }
    
}
