using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    Rigidbody2D rb;
    public float leftPos;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.x <= leftPos)
        {
            rb.velocity = new Vector2(speed, 0);
        }

        if (transform.position.x >= -leftPos)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }
}
