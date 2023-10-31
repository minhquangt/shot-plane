using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReward : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Tag.BorderBullet))
        {
            Destroy(gameObject);
        }
    }
}
