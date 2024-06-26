using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerObject : MonoBehaviour
{
    
    private float playerMoveSpeed = 3.5f;

    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    public Animator animator;

    public GameObject gameOverPanel; 

    public List<int> savedRooms = new List<int>(5);

    public int meleDamage = 3;
    public int rangedDamage = 2;
    public int rangedSpeed = 5;
    
    public int numberOfDreamCatchers = 0;


    public CapsuleCollider2D weaponCollider;

    public BoxCollider2D characterCollider;

    public int health = 5;
    public int gems = 0;
    private bool isInvincible = false;
    private float invincibilityTimer = 0.0f;
    private float invincibilityDuration = 1.0f; 
    private float flashTimer = 0.05f;
    private Color originalColor;
    private Color invincibleColor = Color.red;

    public Image catcherImage; 

    private float weaponSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.33f, 0.24f, 0.0f);
        originalColor = sr.color;
        gems = 0;
        Debug.Log(gems);
        for (int i = 0; i < 5; i++)
        {
            savedRooms.Add(0); // Replace 0 with the default value you want
        }

        // gam = GameObject.FindWithTag("YourTagHere");
        
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            if(savedRooms[0] == 0){
                SaveRoom(0);
            }else{
                teleportToRoom(savedRooms[0]);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
    
            if(savedRooms[1] == 0){
                SaveRoom(1);
            }else{
                teleportToRoom(savedRooms[1]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            
            if(savedRooms[2] == 0){
                SaveRoom(2);
            }else{
                teleportToRoom(savedRooms[2]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(savedRooms[3] == 0){
                SaveRoom(3);
            }else{
                teleportToRoom(savedRooms[3]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
           if(savedRooms[4] == 0){
                SaveRoom(4);
            }else{
                teleportToRoom(savedRooms[4]);
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
                    if (brainSlayerScript.Health > meleDamage){
                        brainSlayerAnimator.SetTrigger("Hit");
                        attack = true;

                    } else if (brainSlayerAnimator.GetBool("alive")){
                        brainSlayerAnimator.SetBool("alive", false);
                        other.collider.enabled = false;
                        brainSlayerAnimator.SetTrigger("Death");
                        
                    }

                    brainSlayerScript.Health -= meleDamage;
                    
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
                    if (angelOfDeathScript.Health > meleDamage){
                        angelOfDeathAnimator.SetTrigger("Hit");
                        attack = true;

                    } else if (angelOfDeathAnimator.GetBool("alive")){
                        angelOfDeathAnimator.SetBool("alive", false);
                        
                        angelOfDeathAnimator.SetTrigger("Death");
                        
                    }

                    angelOfDeathScript.Health -= meleDamage;
                    
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

        if(other.gameObject.CompareTag("Demon")){
            Animator demonAnimator = other.gameObject.GetComponent<Animator>();
            DemonScript demonScript = other.gameObject.GetComponent<DemonScript>();
            bool attack = false;
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.otherCollider == weaponCollider && demonAnimator.GetBool("alive"))
                {
                    if (demonScript.Health > meleDamage){
                        demonAnimator.SetTrigger("Hit");
                        attack = true;

                    } else if (demonAnimator.GetBool("alive")){
                        demonAnimator.SetBool("alive", false);
                        
                        demonAnimator.SetTrigger("Death");
                        
                    }

                    demonScript.Health -= meleDamage;
                    
                    break; // Stop checking after the first match
                }
                if (contact.otherCollider == characterCollider && demonAnimator.GetBool("alive"))
                    if(!attack) {
                        if (!this.isInvincible) {

                            OnHit(3);
                            
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
        if(this.health<=0){
            PlayerDeath();
        }
    }

    private void PlayerDeath(){
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    private void SaveRoom(int position) {

        if(numberOfDreamCatchers > 0){
            GameObject targetObject = GameObject.Find("RoomController");
            RoomController targetScript = targetObject.GetComponent<RoomController>();
            int seed = targetScript.GetSeed();
            bool alreadySaved = false;
            for (int i = 0; i < 5; i++)
            {
                if(savedRooms[i] == seed){
                    alreadySaved = true;
                }
            }

            if (!alreadySaved){

                savedRooms[position] = seed;
                GameObject imageGameObject = GameObject.Find("Catcher"+position);
                catcherImage = imageGameObject.GetComponent<Image>();
                Sprite newSprite = Resources.Load<Sprite>("dreamCatcher");
                catcherImage.sprite = newSprite;

                
                GameObject dreamCatcher = Resources.Load<GameObject>("DreamCatcher");
                Instantiate(dreamCatcher, new Vector3(0, 0, 0), Quaternion.identity);
                numberOfDreamCatchers --;
            }
        }
    }

    private void teleportToRoom(int seed){
        GameObject targetObject = GameObject.Find("RoomController");
        RoomController targetScript = targetObject.GetComponent<RoomController>();
        targetScript.GenerateRoom(seed);
        GameObject dreamCatcher = Resources.Load<GameObject>("DreamCatcher");
        Instantiate(dreamCatcher, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
