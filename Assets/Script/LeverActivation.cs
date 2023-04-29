using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActivation : MonoBehaviour
{
    public GameObject lever;
    public GameObject objectToActivate;
    bool isActivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon") && !isActivated)
        {
            Flip();
            objectToActivate.GetComponent<ObjectMovement>().shouldMove = true;
            isActivated = true;
        }
    }
    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
