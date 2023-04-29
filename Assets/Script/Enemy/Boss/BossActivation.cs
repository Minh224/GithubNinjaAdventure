using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    public GameObject BossGo;

    void Start()
    {
        BossGo.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BossUI.intance.BossActivator();
            StartCoroutine(WaiForBoss());
        }
    }
    IEnumerator WaiForBoss()
    {
        var currentSpeed = Playercontroller.instance.speed;
        yield return new WaitForSeconds(3.0f);
        BossGo.SetActive(true);

        Destroy(gameObject);


    }
}
