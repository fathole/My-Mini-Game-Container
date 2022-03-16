using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private BoxCollider2D BoxCollider2D;
    private void Start()
    {
        BoxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            GameObject.Destroy(collision.gameObject);
        }
    }
}
