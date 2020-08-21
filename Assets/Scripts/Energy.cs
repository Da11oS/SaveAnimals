using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Energy : MonoBehaviour
{

    private Image _energyImage;
    public static float StartEnergy { get { return 1; } }
    private float _energy;
    [Range(0,1)]
    [SerializeField]  private float _costRate;
    private Tilemap _terrainTileMap;
    private TerraineGenerator _terrainGenerator;
 

    void Start()
    {
        _energyImage = GetComponent<Image>();
        _energy = StartEnergy;
        _terrainGenerator = FindObjectOfType<TerraineGenerator>();
        _terrainTileMap = _terrainGenerator.Tilemap;
       // Instantiate(_energyPoint, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        if (_energy > 0)
        {
            _energy -= Time.deltaTime * _costRate;
            _energyImage.fillAmount = _energy;
        }
        //else
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestoreEnergy()
    {
        _energy = StartEnergy;
    }


}
