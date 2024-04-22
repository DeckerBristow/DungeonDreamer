using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    public PlayerObject player; // Reference to the PlayerObject script
    public Sprite fullHeart; // Full heart sprite
    public Sprite emptyHeart; // Empty heart sprite
    public Image heartPrefab; // Reference to the heart image prefab
    public GameObject shopMenuUI;
    private Image[] hearts; // Array to store heart Image components

    void Start()
    {
        InitializeHearts(player.health);
    }

    void Update()
    {
        UpdateHearts(player.health);
    }

    void InitializeHearts(int numHearts)
    {
        hearts = new Image[numHearts];
        for (int i = 0; i < numHearts; i++)
        {
            hearts[i] = Instantiate(heartPrefab, transform);
            hearts[i].rectTransform.anchoredPosition = new Vector2(180 - i*25, 90); 
            hearts[i].sprite = fullHeart; 
        }
    }

    void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }

    public GameObject GetShopUI() {
        return shopMenuUI;
    }
}
