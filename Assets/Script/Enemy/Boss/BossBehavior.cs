using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossBehavior : MonoBehaviour
{
    public Transform[] transforms;
    public GameObject flame;
    public float timeToShoot, countdown;
    public float timeToTP, countdownToTP;

    public float bossHealth, currentHealth;
    public Image HealthImg;
    private void Start()
    {
        transform.position = transforms[1].position;
        countdown = timeToShoot;
        countdownToTP = timeToTP;

    }
    private void Update()
    {

        Countdowns();
        DamageBoss();
        BossScale();
    }
    public void Countdowns()
    {
        countdown -= Time.deltaTime;
        countdownToTP -= Time.deltaTime;

        if (countdown <= 0f)
        {
            ShootPlayer();
            countdown = timeToShoot;
            TelePort();
        }
        if (countdownToTP <= 0)
        {
            countdownToTP = timeToTP;
            TelePort();
        }
    }    
    public void ShootPlayer()
    {     
            GameObject spell = Instantiate(flame, transform.position, Quaternion.identity);     
    }    
   
    public void TelePort()
    {
        var initialPosition = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosition].position;
    }  
    public void DamageBoss()
    {
        currentHealth = GetComponent<EnemySke>().healthPoints;
        HealthImg.fillAmount = currentHealth / bossHealth;
    }
    public void BossScale()
    {
        if(transform.position.x > Playercontroller.instance.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void OnDestroy()
    {
        BossUI.intance.BossDeactivator();
    }
}
