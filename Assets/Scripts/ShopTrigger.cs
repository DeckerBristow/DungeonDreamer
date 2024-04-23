using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private GameObject canvas;  
    private HeartController script;
    private GameObject shopMenuUI;

    void Start() {
        canvas = GameObject.Find("Canvas");
        script = canvas.GetComponent<HeartController>();
        shopMenuUI = script.GetShopUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  
        {
            Time.timeScale = 0;
            shopMenuUI.SetActive(true);  
        }
    }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //     {
    //         shopMenuUI.SetActive(false);  
    //     }
    // }
}
