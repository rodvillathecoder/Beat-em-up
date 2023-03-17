using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]


public class PlayerManager : Fighter
{
    Vector2 movimiento;

    // Update is called once per frame
    void Update()
    {
        movimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(movimiento.x != 0 || movimiento.y != 0)
        {
            spr.flipX = movimiento.x < 0;
            
            
            anim.SetBool("IsWalking", true);

            rd.velocity = new Vector2(movimiento.x * speedX, movimiento.y * speedY);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, LimiteX.x, LimiteX.y), Mathf.Clamp(transform.position.y, LimitesY.y, LimitesY.x), transform.position.z);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }


        //ataque
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("IsAttacking", true);
            StartCoroutine(Puños());
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }
}
