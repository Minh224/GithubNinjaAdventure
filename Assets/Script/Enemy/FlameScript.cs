using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
    float moveSpeed;
    Rigidbody2D rb2d;
    Vector2 moveDirection ;
    Playercontroller target;
    void Start()
    {
        moveSpeed = GetComponent<EnemySke>().speed;
        rb2d = GetComponent<Rigidbody2D>();
        target = Playercontroller.instance;

        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed ;
        rb2d.velocity = new Vector2(moveDirection.x , moveDirection.y);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStates player = collision.GetComponent<PlayerStates>();
            if (player != null)
            {
                player.TakeDamage(1.5f);

            }
        }
    }
}
