using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    
    private float playerMoveSpeed = 3.5f;

    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    public Animator animator;

    public int meleeDamage = 3;
    

    public CapsuleCollider2D weaponCollider;

    public BoxCollider2D characterCollider;

    public int health = 5;
    private bool isInvincible = false;
    private float invincibilityTimer = 0.0f;
    private float invincibilityDuration = 1.0f; 
    private float flashTimer = 0.05f;
    private Color originalColor;
    private Color invincibleColor = Color.red;

    private float weaponSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.33f, 0.24f, 0.0f);
        originalColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * playerMoveSpeed;
        vertical = Input.GetAxis("Vertical") * playerMoveSpeed;

        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(horizontal),Mathf.Abs(vertical)));

        // animator.SetFloat("PlayerSpeed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        if ((isFacingRight && horizontal < 0.0f) || (!isFacingRight && horizontal > 0.0f)) {
            isFacingRight = !isFacingRight;
        }

        
        
        if (isInvincible) {
            // Debug.Log("invincible!");
            invincibilityTimer -= Time.deltaTime;
            flashTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0) {
                sr.enabled = true;
                isInvincible = false;
                flashTimer = 0.15f;
                sr.color = originalColor;
            }
            else if (flashTimer <= 0) {
                sr.enabled = !sr.enabled;
                flashTimer = 0.15f;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, vertical);
        sr.flipX = !isFacingRight;


        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
            Vector2 offset = weaponCollider.offset;

            if (isFacingRight){
                if(offset.x >=0){
                    offset.x += weaponSpeed;
                    offset.y += weaponSpeed/3;
                }
                else{
                    offset.x *= -1;
                    offset.x += weaponSpeed;
                    offset.y += weaponSpeed/3;
                }
            }
            else{
                if(offset.x < 0){
                    offset.x -= weaponSpeed;
                    offset.y += weaponSpeed/3;
                }
                else{
                    offset.x *= -1;
                    offset.x -= weaponSpeed;
                    offset.y += weaponSpeed/3;
                }
            }

            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1){
                offset.x = 0;
                offset.y = -.05f;
            }
            weaponCollider.offset = offset;
        }

    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("BrainSlayer")){
            Animator brainSlayerAnimator = other.gameObject.GetComponent<Animator>();
            BrainSlayerScript brainSlayerScript = other.gameObject.GetComponent<BrainSlayerScript>();
            bool attack = false;
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.otherCollider == weaponCollider)
                {
                    if (brainSlayerScript.Health > meleeDamage){
                        brainSlayerAnimator.SetTrigger("Hit");
                        attack = true;

                    } else if (brainSlayerAnimator.GetBool("alive")){
                        brainSlayerAnimator.SetBool("alive", false);
                        other.collider.enabled = false;
                        brainSlayerAnimator.SetTrigger("Death");
                        
                    }

                    brainSlayerScript.Health -= meleeDamage;
                    
                    break; // Stop checking after the first match
                }
                if (contact.otherCollider == characterCollider && brainSlayerAnimator.GetBool("alive"))
                    if(!attack) {
                        if (!this.isInvincible) {
                            OnHit(1);
                        }
            }
            }
            
        }




        if(other.gameObject.CompareTag("AngelOfDeath")){
            Animator angelOfDeathAnimator = other.gameObject.GetComponent<Animator>();
            AngelOfDeathScript angelOfDeathScript = other.gameObject.GetComponent<AngelOfDeathScript>();
            bool attack = false;
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.otherCollider == weaponCollider && angelOfDeathAnimator.GetBool("alive"))
                {
                    if (angelOfDeathScript.Health > meleeDamage){
                        angelOfDeathAnimator.SetTrigger("Hit");
                        attack = true;

                    } else if (angelOfDeathAnimator.GetBool("alive")){
                        angelOfDeathAnimator.SetBool("alive", false);
                        
                        angelOfDeathAnimator.SetTrigger("Death");
                        
                    }

                    angelOfDeathScript.Health -= meleeDamage;
                    
                    break; // Stop checking after the first match
                }
                if (contact.otherCollider == characterCollider && angelOfDeathAnimator.GetBool("alive"))
                    if(!attack) {
                        if (!this.isInvincible) {
                            OnHit(2);
                        }
            }
            }
            
        }



    }

    private void OnHit(int damage) {
        // Debug.Log("Got hit!");
        this.health -= damage;
        this.isInvincible = true;
        this.invincibilityTimer = this.invincibilityDuration;
        sr.color = invincibleColor;
    }
}
