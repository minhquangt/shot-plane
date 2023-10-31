using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject player;
    public bool isPlayer;
    public GameObject boom;
  
    public float speed;

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            Vector3 from = transform.up;
            Vector3 to = player.transform.position - transform.position;

            float angle = Vector3.SignedAngle(from, to, transform.forward);
            transform.Rotate(0.0f, 0.0f, angle);
            if (isPlayer)
            {
                Boom();
            }
        }
    }

    void Boom()
    {
        Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.Tag.Player)
        {
            isPlayer = true;
        }
    }
}
