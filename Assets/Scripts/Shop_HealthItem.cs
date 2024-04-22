using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_HealthItem : MonoBehaviour
{
    public PlayerObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBuy() {
        // 1. reduce gold

        // 2. add heart
        if(player.health < 5) {
            player.health += 1;
        }
    }
}
