using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,1)]public float Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerEnergy>() != null)
        {
            Debug.Log("Damage");
            collision.gameObject.GetComponent<PlayerEnergy>().ReduceEnergy(Damage);
        }
    }
}
