using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    // Start is called before the first frame update
    // public float speed = 5f;
    private Vector2 movementDirection;
    public GameObject fireBall;
    public PlayerObject player;
    int fireBallDamage;
    int speed;
    
    public void SetDirection(Vector2 direction) {
        movementDirection = direction.normalized;
    }
    void Start()
    {
        fireBallDamage = player.rangedDamage;
        speed = player.rangedSpeed;
        fireBall.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementDirection * (float)speed * Time.deltaTime);
        
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
        if(other.gameObject.CompareTag("AngelOfDeath")){
            Animator angelOfDeathAnimator = other.gameObject.GetComponent<Animator>();
            AngelOfDeathScript angelOfDeathScript = other.gameObject.GetComponent<AngelOfDeathScript>();
            bool attack = false;
            foreach (ContactPoint2D contact in other.contacts)
            {
                
                if (angelOfDeathScript.Health > fireBallDamage){
                    angelOfDeathAnimator.SetTrigger("Hit");
                    attack = true;

                } else if (angelOfDeathAnimator.GetBool("alive")){
                    angelOfDeathAnimator.SetBool("alive", false);
                    angelOfDeathAnimator.SetTrigger("Death");
                    if(angelOfDeathAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1){
                        
                    }
                }

                angelOfDeathScript.Health -= fireBallDamage;
                
                break; // Stop checking after the first match
                
                
            }
            
        }


        if(other.gameObject.CompareTag("Demon")){
            Animator demonAnimator = other.gameObject.GetComponent<Animator>();
            DemonScript demonScript = other.gameObject.GetComponent<DemonScript>();
            bool attack = false;
            foreach (ContactPoint2D contact in other.contacts)
            {
                
                if (demonScript.Health > fireBallDamage){
                    demonAnimator.SetTrigger("Hit");
                    attack = true;

                } else if (demonAnimator.GetBool("alive")){
                    demonAnimator.SetBool("alive", false);
                    demonAnimator.SetTrigger("Death");
                    if(demonAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1){
                        
                    }
                }

                demonScript.Health -= fireBallDamage;
                
                break; // Stop checking after the first match
                
                
            }
            
        }

     }

     }



