using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float secondsToDestroy;
    
    private void Start()
    {
        Destroy(gameObject, secondsToDestroy);
    }
}
