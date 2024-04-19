using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    private Vector2 movementDirection;
    public GameObject fireBall;
    public int fireBallDamage = 2;
    
    public void SetDirection(Vector2 direction) {
        movementDirection = direction.normalized;
    }
    void Start()
    {
        fireBall.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime);
        
    }

     private void OnCollisionEnter2D(Collision2D other){
        Destroy(gameObject);


        if(other.gameObject.CompareTag("BrainSlayer")){
            Animator brainSlayerAnimator = other.gameObject.GetComponent<Animator>();
            BrainSlayerScript brainSlayerScript = other.gameObject.GetComponent<BrainSlayerScript>();
            bool attack = false;
            foreach (ContactPoint2D contact in other.contacts)
            {
                
                if (brainSlayerScript.Health > fireBallDamage){
                    brainSlayerAnimator.SetTrigger("Hit");
                    attack = true;

                } else if (brainSlayerAnimator.GetBool("alive")){
                    brainSlayerAnimator.SetBool("alive", false);
                    brainSlayerAnimator.SetTrigger("Death");
                    if(brainSlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1){
                        
                    }
                }

                brainSlayerScript.Health -= fireBallDamage;
                
                break; // Stop checking after the first match
                
                
            }
            
        }



     }
}
