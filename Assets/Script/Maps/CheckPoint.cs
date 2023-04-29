using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private AudioSource CheckPointSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.transform.tag == "Player")
        {
            CheckPointSound.Play();
            PlayerManager.lastCheckPointPos = transform.position;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
