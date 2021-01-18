using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,1)]public float Damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.gameObject.GetComponent<PlayerEnergy>();
        if (target != null)
        {
            target.ReduceEnergy(Damage);
        }
    }
}
