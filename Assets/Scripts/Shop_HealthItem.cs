using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop_HealthItem : MonoBehaviour
{
    private PlayerObject player;
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
        if (player.gems < 5 || player.health == 5) {
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
        // 1. reduce gold
        if(player.gems >= 5 && player.health < 5) {
            player.gems -= 5;
            GameObject textObject = GameObject.FindGameObjectWithTag("ScoreTag");
            TextMeshProUGUI textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = ""+player.gems;
            UpdateButtonColor();
            // 2. add heart
            player.health += 1;
            // 3. feedback
            GameObject feedbackObj = GameObject.Find("PurchaseFeedback");
            if(feedbackObj != null) {
                if(feedbackObj.GetComponent<TextMeshProUGUI>().color.a == 0) {
                    feedbackObj.GetComponent<FeedbackController>().Activate();
                }
                TextMeshProUGUI feedbackText = feedbackObj.GetComponent<TextMeshProUGUI>();
                feedbackText.text = "Purchased Health!";
                feedbackObj.GetComponent<FeedbackController>().ChangedText();
            }
        }
    }
}
