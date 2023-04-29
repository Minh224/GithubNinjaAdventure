using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BossUI : MonoBehaviour
{
    public GameObject bossPanel;
    public GameObject muros;

    public static BossUI intance;



    private void Awake()
    {
        if(intance == null)
        {
            intance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1;

        bossPanel.SetActive(false);
        muros.SetActive(false);
    }

    public void BossActivator()
    {
        bossPanel.SetActive(true);
        muros.SetActive(true);
    }
    public void BossDeactivator()
    {
        bossPanel.SetActive(false);
        muros.SetActive(false);
        StartCoroutine(BossDefeadted());
    }
  IEnumerator BossDefeadted()
    {
        var velocity = Playercontroller.instance.GetComponent<Rigidbody2D>().velocity;
        Vector2 originalSpeed = velocity;
        Playercontroller.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity.y);
        Playercontroller.instance.enabled = false;
        yield return new WaitForSeconds(3);
        Playercontroller.instance.enabled = true;
        velocity = originalSpeed;
    }
}
