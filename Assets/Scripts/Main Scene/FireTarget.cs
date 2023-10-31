using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTarget : MonoBehaviour
{
    public float speed;
    public GameObject enemy;
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag(Constants.Tag.Enemy);
    }

    void Update()
    {
        if (enemy == null || enemy.transform.position.y >= 4.7f)
        {
            Destroy(gameObject);
            return;
        }
        
        else if (enemy.transform.position.y <= 4.7f)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, Time.deltaTime * speed);
        }
    }
}
