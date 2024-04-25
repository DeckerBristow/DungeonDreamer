using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelOfDeathScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int Health = 15;

    [SerializeField] private Rigidbody2D playerRb;

    [SerializeField] private Rigidbody2D AngelOfDeathRb;

    public Animator animator;

    private Vector2 targetPosition;
    private bool isMoving = false;
    public float speed = 8f; // Adjust speed as needed
    private float followSpeed = 1f;

    private BoxCollider2D boxCollider;

    public GameObject gameObjectWithCollider;


    void Start()
    {
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            // Move towards the target position
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

            // Check if the AngelOfDeath has reached the target position
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Stop moving
                isMoving = false;
                animator.SetBool("isFlying", false);
            }
        }
        else
        {
            // Calculate the distance between AngelOfDeath and the player
            float distance = Vector2.Distance(transform.position, playerRb.transform.position);

            // If the distance is greater than 7, set the target position to the player's current position and start moving
            if (distance > 7 && animator.GetBool("alive"))
            {
                targetPosition = playerRb.transform.position;
                isMoving = true;
                animator.SetBool("isFlying", true);
            } else if (distance < 5 && animator.GetBool("alive"))
        {
            // Follow the player slowly
            float step = followSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, playerRb.transform.position, step);
        }
        }

        // if(animator.GetBool("alive")){
        //     if (AngelOfDeathRb.transform.position.x > playerRb.transform.position.x){
        //         AngelOfDeathRb.transform.Translate((-1*movementSpeed), 0, 0);
        //         animator.SetBool("isWalkingLeft", true);
        //         animator.SetBool("isWalkingRight", false);
                

        //     }
        //     if (AngelOfDeathRb.transform.position.x < playerRb.transform.position.x){
        //         AngelOfDeathRb.transform.Translate((movementSpeed), 0, 0);
        //         animator.SetBool("isWalkingRight", true);
        //         animator.SetBool("isWalkingLeft", false);


        //     }
        //     if (AngelOfDeathRb.transform.position.y > (playerRb.transform.position.y-.5)){
        //         AngelOfDeathRb.transform.Translate(0, (-1*movementSpeed/2), 0);

        //     }
        //     if (AngelOfDeathRb.transform.position.y < playerRb.transform.position.y-.5){
        //         AngelOfDeathRb.transform.Translate(0, (movementSpeed/2), 0);

        //     }
        // }

        
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void Died()
    {
        boxCollider.enabled = false;
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        GameObject gem = Resources.Load<GameObject>("Gem");
        Instantiate(gem, new Vector3(x, y, 0), Quaternion.identity);
        Instantiate(gem, new Vector3(x+1, y, 0), Quaternion.identity);
        
    }
}


