using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerObject player = other.gameObject.GetComponent<PlayerObject>();
            
            player.gems += 1;
            GameObject textObject = GameObject.FindGameObjectWithTag("ScoreTag");
            TextMeshProUGUI textMesh = textObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = ""+player.gems;
            Destroy(gameObject);
        
        }
    }
}
