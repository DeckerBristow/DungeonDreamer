using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    
    public float playerMoveSpeed = 6.0f;

    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.33f, 0.24f, 0.0f);
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

        if (Input.GetButtonDown("Fire1"))
        {
            // launch a melee attack in direction of mouse cursor

            // animator.Play("attack");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, vertical);
        sr.flipX = !isFacingRight;
    }
}
