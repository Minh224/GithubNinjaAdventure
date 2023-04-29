using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeHealth : MonoBehaviour
{
    EnemySke enemyske;
    public bool isDamaged;
    public GameObject deathEffect;
    SpriteRenderer sprite;
    Blink material;
    public Rigidbody2D rb;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        rb = GetComponent<Rigidbody2D>();
        enemyske = GetComponent<EnemySke>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon")&& !isDamaged)
        {
            enemyske.healthPoints -= 15f;
            StartCoroutine(Damager());
            if(enemyske.healthPoints <= 0 )
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }    
        }
           
        
    }
    IEnumerator Damager()
    {
        isDamaged = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(0.3f);
        sprite.material = material.original;
        isDamaged = false;

     
    }
}
