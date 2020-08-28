using System;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private Image _energyImage;
    private PlayerEnergy _target;
 
    void Start()
    {
        _target = FindObjectOfType<PlayerEnergy>();
        _energyImage = GetComponent<Image>();
    }


    private void OnEnable()
    {
        _target.OnEnergyChange += UpdateValue;
    }
    private void OnDisable()
    {
        _target.OnEnergyChange -= UpdateValue;
    }
 

    public void UpdateValue(float value)
    {
        if (_target == null) return;
        _energyImage.fillAmount = value;
    }

}
