using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static System.Int32;
using System;

public class PlayerAttack : MonoBehaviour
{

    public Animator animator;

    public  Rigidbody2D rb;

    public GameObject fireBallPrefab;

    public Vector2 lastDirection;

    void Update()
    {
        Vector2 currentVelocity = rb.velocity;
        if (currentVelocity != Vector2.zero) {
            lastDirection = currentVelocity.normalized;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)){
            
            GameObject fireBall = Instantiate(fireBallPrefab, new Vector3(rb.transform.position.x, rb.transform.position.y, 0), Quaternion.identity);
            FireBallMovement fireballMovement = fireBall.GetComponent<FireBallMovement>();
    
            fireballMovement.SetDirection(lastDirection);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

    }
}
