using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject shopPhysical;
    public RectTransform[] itemPositions;
    private List<GameObject> shopItems; 

    void Start()
    {
        shopPanel.SetActive(false);
        shopPanel.transform.SetAsLastSibling();
        InitializeShopItems();
    }
    
    private void InitializeShopItems() {
        shopItems = new List<GameObject>();
        foreach (Transform child in transform) {
            if (child.CompareTag("ShopItem")) {
                shopItems.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
    }

    public void CloseMenu() {
        shopPanel.SetActive(false);
        shopPhysical.GetComponent<ShopTrigger>().InvokeCloseMenu();
        Time.timeScale = 1;
    }

    void SetItemPosition1(RectTransform itemPosition) {
        itemPosition.anchorMin = new Vector2(0, 0);
        itemPosition.anchorMax = new Vector2(1, 1);
        itemPosition.pivot = new Vector2(0.5f, 0.5f);
        itemPosition.offsetMin = new Vector2(7.866385f, 66.94195f); // Left, Bottom
        itemPosition.offsetMax = new Vector2(-6.030814f, -116.9419f); // -Right, -Top
    }

    void SetItemPosition2(RectTransform itemPosition) {
        itemPosition.anchorMin = new Vector2(0, 0);
        itemPosition.anchorMax = new Vector2(1, 1);
        itemPosition.pivot = new Vector2(0.5f, 0.5f);
        itemPosition.offsetMin = new Vector2(7.866385f, 12.14196f); // Left, Bottom
        itemPosition.offsetMax = new Vector2(-6.030814f, -171.7419f); // -Right, -Top
    }

    public void generateShopItems(ref System.Random random) {
        PlayerObject player = GameObject.FindWithTag("Player").GetComponent<PlayerObject>();
        List<GameObject> eligibleShopItems = new List<GameObject>(shopItems);

        foreach (GameObject item in shopItems) {
            item.SetActive(false);
        }

        if(player.meleDamage == 21) {
            eligibleShopItems.RemoveAll(item => item.name == "Shop_SwordUpgrade");
        }
        if(player.rangedDamage == 13) {
            eligibleShopItems.RemoveAll(item => item.name == "Shop_FireballUpgrade");
        }
        if(player.rangedSpeed == 25) {
            eligibleShopItems.RemoveAll(item => item.name == "Shop_RangedSpeedUpgrade");
        }

        int itemCount = random.Next(1, 100) < 65 ? 1 : 2; // 65% chance for 1 item, else 2 items
        List<int> chosenIndexes = new List<int>();
        
        while (chosenIndexes.Count < itemCount && chosenIndexes.Count < eligibleShopItems.Count) {
            int index = random.Next(eligibleShopItems.Count);
            if (!chosenIndexes.Contains(index)) {
                chosenIndexes.Add(index);
            }
        }
        for (int i = 0; i < chosenIndexes.Count; i++) {
            GameObject item = eligibleShopItems[chosenIndexes[i]];
            // item.transform.position = itemPositions[i].position;
            if(i == 0) {
                SetItemPosition1(item.GetComponent<RectTransform>());
            } else if (i == 1) {
                SetItemPosition2(item.GetComponent<RectTransform>());
            }
            item.SetActive(true); 
        }
    }
}
