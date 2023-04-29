using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int diamond = 0;
    [SerializeField] private Text diamondText;

    [SerializeField] private AudioSource pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Diamond"))
        {
            pickupSound.Play();
            Destroy(collision.gameObject);
            diamond++;
            diamondText.text = " : " + diamond;
        }    
    }
}
