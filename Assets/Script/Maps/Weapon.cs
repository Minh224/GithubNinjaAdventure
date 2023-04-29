using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public Transform firePoint;
   public GameObject bulletPrefab;
   public float fireRate = 0.2f; // Thời gian delay giữa các lần bắn
   private bool canShoot = true; // Biến kiểm soát việc có thể bắn hay không
   [SerializeField] private AudioSource ShootSound;


    // Update is called once per frame
    void Update()
   {
       if(Input.GetButtonDown("Fire1")&& canShoot )
       {
            ShootSound.Play();
            StartCoroutine(ShootDelay());
        }
   }
    IEnumerator ShootDelay()
    {
        // Đặt biến canShoot thành false để ngăn chặn bắn trong thời gian delay
        canShoot = false;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Chờ trong khoảng thời gian fireRate
        yield return new WaitForSeconds(fireRate);
        // Đặt biến canShoot thành true để cho phép bắn tiếp
        canShoot = true;
    }

}
