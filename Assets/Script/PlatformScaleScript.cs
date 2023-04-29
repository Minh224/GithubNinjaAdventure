using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScaleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(gameObject.GetComponent<ObjectMovement>().willDestroy)
            {
                gameObject.GetComponent<ObjectMovement>().startcd = true;
            }
            collision.transform.SetParent(this.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            collision.transform.SetParent(null);
        }
    }
}
