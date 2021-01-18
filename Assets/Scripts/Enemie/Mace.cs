using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mace : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float _impulseForce;
    [SerializeField]private LayerMask _grassLayer;
    private Rigidbody2D _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TilemapCollider2D>() != null)
        {
            Debug.Log("grass");
            _rigidbody.AddForce(Vector2.up * _impulseForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
