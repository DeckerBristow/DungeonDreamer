using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // [System.Serializable]
    // public struct ShopItem
    // {
    //     public Sprite itemSprite;
    //     public int price;
    //     public string itemName;
    // }

    // public ShopItem[] itemsForSale;
    // public GameObject itemTemplate;
    public GameObject shopPanel;

    void Start()
    {
        shopPanel.SetActive(false);
        // ShopItem healthItem = {itemSprite: ''} as ShopItem;
        // foreach (var item in itemsForSale)
        // {
        //     GameObject itemObj = Instantiate(itemTemplate, shopPanel);
        //     itemObj.GetComponent<Image>().sprite = item.itemSprite;
        //     itemObj.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
        //     itemObj.transform.Find("Price").GetComponent<Text>().text = $"Price: {item.price}";
        //     // Add more setup code here, e.g., button listeners
        // }
    }
    
    public void CloseMenu() {
        shopPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
