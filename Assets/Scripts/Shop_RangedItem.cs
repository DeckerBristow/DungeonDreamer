using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using System;
using TMPro;

public class Shop_RangedItem : MonoBehaviour
{
    private PlayerObject player;
    public int[] rangedLevels = new int[] { 2, 3, 5, 8, 13 };
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
        if (player.gems < 35 || player.rangedDamage == 13) {
            if(buyButton == null) {
                buyButton = transform.Find("Button").GetComponent<Button>();
            }
            buyButton.interactable = false;
            buyButton.image.color = notPurchasableColor;
        } else {
            if(buyButton == null) {
                buyButton = transform.Find("Button").GetComponent<Button>();
            }
            buyButton.interactable = true;
            buyButton.image.color = Color.white;
        }
    }

    public void onBuy() {
        if(player == null) {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerObject>();
        }
        Debug.Log(player);
        // 1. reduce gold
        Debug.Log(player.gems);
        if(player.gems >= 35) {
            player.gems -= 35;
            Debug.Log(player.gems);
            GameObject textObject = GameObject.FindGameObjectWithTag("ScoreTag");
            TextMeshProUGUI textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = ""+player.gems;
            UpdateButtonColor();
            // 2. increase damage 
            int currentLevel = Array.IndexOf(rangedLevels, player.rangedDamage);
            if (currentLevel < 4) {
                currentLevel++;
            }
            player.rangedDamage = rangedLevels[currentLevel];
            // 3. feedback
            GameObject feedbackObj = GameObject.Find("PurchaseFeedback");
            if(feedbackObj != null) {
                if(feedbackObj.GetComponent<TextMeshProUGUI>().color.a == 0) {
                    feedbackObj.GetComponent<FeedbackController>().Activate();
                }
                TextMeshProUGUI feedbackText = feedbackObj.GetComponent<TextMeshProUGUI>();
                feedbackText.text = "Upgraded Fireball DMG To Level " + (currentLevel + 1) + "!";
                feedbackObj.GetComponent<FeedbackController>().ChangedText();
            }
        }
    }
}
