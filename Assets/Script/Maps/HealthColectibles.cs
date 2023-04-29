using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthColectibles : MonoBehaviour
{
    [SerializeField] private float healthValue;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerStates>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }

    }
}
