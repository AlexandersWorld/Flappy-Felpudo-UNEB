using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerrotarInimigo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } else if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
