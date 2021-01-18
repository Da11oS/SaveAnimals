using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    // Start is called before the first frame update
    private Level _parentLevel { get => GetComponentInParent<Level>(); }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            //_parentLevel.CurrentType.Finish();
        }
    }
}
