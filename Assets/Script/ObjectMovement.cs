using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed;
    public bool shouldMove;
    public bool shouldWait;
    public float timeToWait;
    public bool canContinue;
    public bool willDestroy;
    public bool startcd;

    public float timeToDestroy;
    public float destroycd;
    bool moveToA;
    bool moveToB;

    private void Start()
    {
        moveToA = true;
        moveToB = false;
        canContinue = true;
        destroycd = timeToDestroy;
        shouldMove = true;

    }
    private void Update()
    {
        if(shouldMove)
        {
            MoveObject();
        }
        if(startcd)
        {
            destroycd -= Time.deltaTime;
            if(destroycd <= 0)
            {
                StartCoroutine(ReactivePlatform());
                destroycd = timeToDestroy;
                startcd = false;
            }
        }
    }

    IEnumerator ReactivePlatform()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        BoxCollider2D[] myColliders = gameObject.GetComponents<BoxCollider2D>();

        foreach (var colliders in myColliders)
        {
            colliders.enabled = false;
        }

        yield return new WaitForSeconds(2f);

        foreach (var colliders in myColliders)
        {
            colliders.enabled = true;
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

    }
    private void MoveObject()
    {
        float distanceToA = Vector2.Distance(transform.position, pointA.position);
        float distanceToB = Vector2.Distance(transform.position, pointB.position);
        if(distanceToA > 0.1f && moveToA)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if(distanceToA < 0.3 && canContinue)
            {
                if(shouldWait)
                {
                    StartCoroutine(Waiter());
                    moveToA = false;
                    moveToB = true;
                }
                else
                {
                    moveToA = false;
                    moveToB = true;
                }

            }
        }

        if (distanceToB > 0.1f && moveToB)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (distanceToB < 0.3 && canContinue)
            {
                if (shouldWait)
                {
                    StartCoroutine(Waiter());
                    moveToA = true;
                    moveToB = false;
                }
                else
                {
                    moveToA = true;
                    moveToB = false;
                }
                
            }
        }
    }
    IEnumerator Waiter()
    {
        shouldMove = false;
        canContinue = false;
        yield return new WaitForSeconds(timeToWait);
        shouldMove = true;
        canContinue = true;
    }
}
