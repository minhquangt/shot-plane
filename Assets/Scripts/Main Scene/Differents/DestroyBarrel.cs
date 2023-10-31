using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrel : MonoBehaviour
{
    [SerializeField]
    Vector2 forceDirection;

    [SerializeField]
    float torque;

    Rigidbody2D rb2d;
    public GameObject parent;
    public float timeDestroy;
    public bool isDestroy;
    void Start()
    {
        isDestroy = true;
        float randTorque = Random.Range(-50, 50);
        float randForceX = Random.Range(forceDirection.x - 50, forceDirection.x + 50);
        float randForceY = Random.Range(forceDirection.y, forceDirection.y + 50);


        forceDirection.x = randForceX;
        forceDirection.y = randForceY;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(forceDirection);
        rb2d.AddTorque(torque);
        if (isDestroy)
        {
            Destroy(parent, timeDestroy);
        }
    }
}
