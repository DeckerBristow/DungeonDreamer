using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Int32;


public class PortalScript : MonoBehaviour
{
     GameObject roomControllerObj;

     
    
    // Start is called before the first frame update
    void Start()
    {
        roomControllerObj = GameObject.FindWithTag("RoomController");
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            GameObject[] existingPortal = GameObject.FindGameObjectsWithTag("Portal");
        foreach (GameObject portal in existingPortal)
        {
            Destroy(portal);
        }
            PlayerObject player = other.gameObject.GetComponent<PlayerObject>();
            GameObject[] existingBrains = GameObject.FindGameObjectsWithTag("BrainSlayer");
        foreach (GameObject brain in existingBrains)
        {
            Destroy(brain);
        }
        GameObject[] existingAngels = GameObject.FindGameObjectsWithTag("AngelOfDeath");
        foreach (GameObject angel in existingAngels)
        {
            Destroy(angel);
        }
        GameObject[] existingCatcher = GameObject.FindGameObjectsWithTag("DreamCatcher");
        foreach (GameObject catcher in existingCatcher)
        {
            Destroy(catcher);
        }
        GameObject[] existingShop = GameObject.FindGameObjectsWithTag("Shop");
        foreach (GameObject shop in existingShop)
        {
            Destroy(shop);
        }
        GameObject[] existingHealth = GameObject.FindGameObjectsWithTag("HealthPickup");
        foreach (GameObject health in existingHealth)
        {
            Destroy(health);
        }
        int newSeed = UnityEngine.Random.Range(1,MaxValue);
        System.Random random = new System.Random(newSeed);
        RoomController roomController = roomControllerObj.GetComponent<RoomController>();
        roomController.generateDecorations(ref random);
        GameObject enemyPrefab = Resources.Load<GameObject>("Demon");
        Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        
        GameObject[] existingTriggers = GameObject.FindGameObjectsWithTag("WallTrigger");
        foreach (GameObject trigger in existingTriggers)
        {
            Destroy(trigger);
        }

        }
    }
}
