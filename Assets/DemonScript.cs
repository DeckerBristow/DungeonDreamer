using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] private Rigidbody2D demonRb;
    public GameObject gameObjectWithCollider;
    private float movementSpeed = .035f;

    private BoxCollider2D boxCollider;

    public int Health = 50;
    public float attackDistance = 2f;

    public GameObject winPanel; 

    public Animator animator;
    private bool isAttacking = false;
    private bool attackAnimationComplete = true;

    void Start()
    {
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if(animator.GetBool("alive")){
            if (Mathf.Abs(demonRb.transform.position.x - playerRb.position.x) < attackDistance)
            {
                if(demonRb.transform.position.x > playerRb.transform.position.x)
                {
                    animator.SetTrigger("Attack");
                    isAttacking = true;
                }
                else
                {
                    animator.SetTrigger("Attack_Right");
                }
                
            }
            if (demonRb.transform.position.x > playerRb.transform.position.x){
                demonRb.transform.Translate((-1*movementSpeed), 0, 0);
                animator.SetBool("isWalkingLeft", true);
                animator.SetBool("isWalkingRight", false);
            }
            if (demonRb.transform.position.x < playerRb.transform.position.x){
                demonRb.transform.Translate((movementSpeed), 0, 0);
                animator.SetBool("isWalkingRight", true);
                animator.SetBool("isWalkingLeft", false);
            }
            if (demonRb.transform.position.y > (playerRb.transform.position.y+1)){
                demonRb.transform.Translate(0, (-1*movementSpeed/2), 0);
            }
            if (demonRb.transform.position.y < playerRb.transform.position.y+1){
                demonRb.transform.Translate(0, (movementSpeed/2), 0);
            }
        }
    }

    private void AttackAnimationComplete()
    {
        attackAnimationComplete = true;
        isAttacking = false;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }

    public void Died()
    {
        boxCollider.enabled = false;

        // float x = gameObject.transform.position.x;
        // float y = gameObject.transform.position.y;
        // GameObject gem = Resources.Load<GameObject>("Gem");
        // Instantiate(gem, new Vector3(x, y, 0), Quaternion.identity);
    }
}
