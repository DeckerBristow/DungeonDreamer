using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shop_SwordItem : MonoBehaviour
{
    public PlayerObject player;
    public int[] meleeLevels = new int[] { 3, 5, 8, 13, 21 };
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

        // 2. increase damage (THIS IS A REALLY DUMB WAY TO DO THIS AND NOT SCALABLE AT ALL BUT IT WILL WORK)
        Debug.Log(player.meleDamage);
        Debug.Log(meleeLevels);
        int currentLevel = Array.IndexOf(meleeLevels, player.meleDamage);
        if (currentLevel < 4) {
            currentLevel++;
        }
        player.meleDamage = meleeLevels[currentLevel];
        Debug.Log("current melee damage: " + player.meleDamage);
    }
}
