using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Shop_RangedSpeedUpgrade : MonoBehaviour
{
    private PlayerObject player;
    private int[] rangedSpeeds = new int[] { 5, 10, 15, 20, 25 };
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
        if (player.gems < 20 || player.rangedSpeed == 25) {
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
        if(player.gems >= 20) {
            player.gems -= 20;
            Debug.Log(player.gems);
            GameObject textObject = GameObject.FindGameObjectWithTag("ScoreTag");
            TextMeshProUGUI textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = ""+player.gems;
            UpdateButtonColor();
            // 2. increase damage 
            // Debug.Log("current ranged speed: " + player.rangedSpeed);
            int currentLevel = Array.IndexOf(rangedSpeeds, player.rangedSpeed);
            // Debug.Log("old level: " + currentLevel);
            if (currentLevel < 4) {
                currentLevel++;
            }
            // Debug.Log("new level: " + currentLevel);
            // Debug.Log("ranged speed to set to: " + rangedSpeeds[currentLevel]);
            player.rangedSpeed = rangedSpeeds[currentLevel];
            // Debug.Log("new ranged speed: " + player.rangedSpeed);
            // 3. feedback
            GameObject feedbackObj = GameObject.Find("PurchaseFeedback");
            if(feedbackObj != null) {
                if(feedbackObj.GetComponent<TextMeshProUGUI>().color.a == 0) {
                    feedbackObj.GetComponent<FeedbackController>().Activate();
                }
                TextMeshProUGUI feedbackText = feedbackObj.GetComponent<TextMeshProUGUI>();
                feedbackText.text = "Upgraded Fireball Speed To Level " + (currentLevel + 1) + "!";
                feedbackObj.GetComponent<FeedbackController>().ChangedText();
            }
        }
    }
}
