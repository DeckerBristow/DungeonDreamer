using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private GameObject canvas;  
    private HeartController script;
    private GameObject shopMenuUI;
    private BoxCollider2D triggerCollider;

    void Start() {
        canvas = GameObject.Find("Canvas");
        script = canvas.GetComponent<HeartController>();
        shopMenuUI = script.GetShopUI();
        triggerCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  
        {
            triggerCollider.enabled = false;
            Time.timeScale = 0;
            shopMenuUI.SetActive(true);  
        }
    }

    public void InvokeCloseMenu() {
        Invoke("EnableTrigger", 1.5f);
    }

    private void EnableTrigger() {
        triggerCollider.enabled = true;
    }
}
