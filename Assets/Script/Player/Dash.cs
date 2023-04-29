using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed = 10f;
    public float dashTime = 0.5f;
    private float dashTimeLeft;
    private bool isDashing = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isDashing)
        {
            isDashing = true;
            dashTimeLeft = dashTime;
        }

        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                transform.Translate(Vector2.right * dashSpeed * Time.deltaTime);
                dashTimeLeft -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
            }
        }
    }
}