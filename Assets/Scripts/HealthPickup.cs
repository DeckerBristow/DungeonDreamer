using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerObject player = other.gameObject.GetComponent<PlayerObject>();
            if(player.health < 5) {
                player.health += 1;
                Destroy(gameObject);
            }
        }
    }
}
