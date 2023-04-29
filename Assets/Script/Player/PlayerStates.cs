using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStates : MonoBehaviour
{
    [SerializeField]
    private float startingHealth;

    public float currenHealth { get; private set; }
    private Animator anim;
    private bool dead;
    bool isInmune;
    public float delayBeforeRestart = 2.0f;

    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource DieSound;
    [SerializeField] private AudioSource HealthSound;


    Rigidbody2D rb;


    private void Awake()
    {

        currenHealth = startingHealth;
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

    }



    public void TakeDamage(float _damage)
    {
        currenHealth = Mathf.Clamp(currenHealth - _damage, 0 , startingHealth);

        if(currenHealth > 0)
        {
            hurtSound.Play();
            anim.SetTrigger("hurt");
        }
        else
        {
            if(!dead)
            {
                DieSound.Play();

                GetComponent<Playercontroller>().enabled = false;
                dead = true;
                StartCoroutine(DieAnimation());

            }

        }
    }
    IEnumerator DieAnimation()
    {
        anim.SetTrigger("die");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        PlayerManager.isGameOver = true;
        gameObject.SetActive(false);
    }

    public void AddHealth(float _value)
    {
        HealthSound.Play();
        currenHealth = Mathf.Clamp(currenHealth + _value, 0, startingHealth);
    }
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
