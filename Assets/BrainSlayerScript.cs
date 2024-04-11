using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainSlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Rigidbody2D brainSlayerRb;
    private float movementSpeed = .025f;

    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (brainSlayerRb.transform.position.x > playerRb.transform.position.x){
            brainSlayerRb.transform.Translate((-1*movementSpeed), 0, 0);
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWalkingRight", false);
            

        }
        if (brainSlayerRb.transform.position.x < playerRb.transform.position.x){
            brainSlayerRb.transform.Translate((movementSpeed), 0, 0);
            animator.SetBool("isWalkingRight", true);
            animator.SetBool("isWalkingLeft", false);


        }
        if (brainSlayerRb.transform.position.y > playerRb.transform.position.y){
            brainSlayerRb.transform.Translate(0, (-1*movementSpeed), 0);

        }
        if (brainSlayerRb.transform.position.y < playerRb.transform.position.y){
            brainSlayerRb.transform.Translate(0, (movementSpeed), 0);

        }
        


    }
}
