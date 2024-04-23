using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Shop_SwordItem : MonoBehaviour
{
    private PlayerObject player;
    public int[] meleeLevels = new int[] { 3, 5, 8, 13, 21 };
    private Button buyButton; 
    private Color notPurchasableColor = new Color(0.33f, 0.33f, 0.33f);
    // Start is called before the first frame update
    void Start()
    {
        buyButton = transform.Find("Button").GetComponent<Button>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerObject>();
        UpdateButtonColor();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonColor();
    }

    private void UpdateButtonColor() {
        if (player.gems < 20 || player.meleDamage == 21) {
            buyButton.interactable = false;
            buyButton.image.color = notPurchasableColor;
        } else {
            buyButton.interactable = true;
            buyButton.image.color = Color.white;
        }
    }

    public void onBuy() {
        // 1. reduce gold
        Debug.Log(player.gems);
        if(player.gems >= 20) {
            player.gems -= 20;
            Debug.Log(player.gems);
            GameObject textObject = GameObject.FindGameObjectWithTag("ScoreTag");
            TextMeshProUGUI textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = ""+player.gems;
            UpdateButtonColor();
            // 2. increase damage 
            int currentLevel = Array.IndexOf(meleeLevels, player.meleDamage);
            if (currentLevel < 4) {
                currentLevel++;
            }
            player.meleDamage = meleeLevels[currentLevel];
        }
    }
}
