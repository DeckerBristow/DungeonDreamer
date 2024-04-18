using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    private Vector2 movementDirection;

    public void SetDirection(Vector2 direction) {
        movementDirection = direction.normalized;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime);
        
    }
}
