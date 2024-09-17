using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart_Controller : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
    //    var player = collision.collider.GetComponent<Sleep>();

    //     if (player)
    //     {
    //         player.isHit(1);
    //     }
        
        Destroy(gameObject);
    }
}
