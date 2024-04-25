using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D playerRb;
    [SerializeField] private Rigidbody2D demonRb;
    public GameObject gameObjectWithCollider;
    private float movementSpeed = .025f;

    private BoxCollider2D boxCollider;

    public int Health = 10;
    public Animator animator;

    void Start()
    {
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(animator.GetBool("alive")){
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
            if (demonRb.transform.position.y > (playerRb.transform.position.y-.5)){
                demonRb.transform.Translate(0, (-1*movementSpeed/2), 0);

            }
            if (demonRb.transform.position.y < playerRb.transform.position.y-.5){
                demonRb.transform.Translate(0, (movementSpeed/2), 0);

            }
        }
        

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
    }
}
