using System.Collections;
using System.Collections.Generic;
using static System.Int32;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;


public class RoomController : MonoBehaviour
{
    private int seed;
    public GameObject enemyPrefab;
    public GameObject shopDisplay;
    public List<Tilemap> leftDecorations;
    public List<Tilemap> rightDecorations;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRoom(int newSeed) {
        if (newSeed == 0) {
            newSeed = UnityEngine.Random.Range(1,MaxValue);
        }
        Debug.Log("Generated new room: " + newSeed);
        this.seed = newSeed;
        System.Random random = new System.Random(this.seed);
        generateEnemies(ref random);
        generateHealthPickups(ref random);
        generateShop(ref random);
        generateDecorations( ref random);

    }

    void DeactivateAllTilemaps()
    {
        foreach (var tilemap in leftDecorations)
        {
            tilemap.gameObject.SetActive(false); // Deactivate each tilemap
        }

         foreach (var tilemap in rightDecorations)
        {
            tilemap.gameObject.SetActive(false); // Deactivate each tilemap
        }
    }

    private void generateDecorations(ref System.Random random){
        DeactivateAllTilemaps();
        int indexLeft = random.Next(leftDecorations.Count); // Get a random index based on the seed
       // DeactivateAllTilemaps(); // Deactivate all tilemaps first
        leftDecorations[indexLeft].gameObject.SetActive(true);

        int indexRight = random.Next(rightDecorations.Count); // Get a random index based on the seed
       // DeactivateAllTilemaps(); // Deactivate all tilemaps first
        rightDecorations[indexRight].gameObject.SetActive(true);
    }

    private void generateEnemies(ref System.Random random) {
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
        int brainSlayers = random.Next(1, 5);
        for (int i = 0; i < brainSlayers; i++) {
            float x = random.Next(-1100, 1100)/100.0f;
            float y = random.Next(-389, 483)/100.0f;
            GameObject enemyPrefab = Resources.Load<GameObject>("BrainSlayer");
            Instantiate(enemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
        int angels = random.Next(0, 2);
        for (int i = 0; i < angels; i++) {
            float x = random.Next(-1100, 1100)/100.0f;
            float y = random.Next(-389, 483)/100.0f;
            GameObject enemyPrefab = Resources.Load<GameObject>("AngelOfDeath");
            Instantiate(enemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    private void generateHealthPickups(ref System.Random random) {
        GameObject[] existingPickups = GameObject.FindGameObjectsWithTag("HealthPickup");
        foreach (GameObject pickup in existingPickups) {
            Destroy(pickup);
        }
        int val = random.Next(1, 100);
        int healthPickups = 0;
        if(val == 100) {
            healthPickups = 4;
        }
        else if (val > 92) {
            healthPickups = 3;
        }
        else if (val > 75) {
            healthPickups = 2;
        }
        else if (val > 55) {
            healthPickups = 1;
        }
        for (int i = 0; i < healthPickups; i++) {
            float x = random.Next(-1100, 1100)/100.0f;
            float y = random.Next(-389, 483)/100.0f;
            GameObject healthPrefab = Resources.Load<GameObject>("HealthPickup");
            Instantiate(healthPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    private void generateShop(ref System.Random random) {
        GameObject[] existingShops = GameObject.FindGameObjectsWithTag("Shop");
        foreach (GameObject shop in existingShops) {
            Destroy(shop);
        }
        if (random.Next(1, 100) > 80) {
            float x = random.Next(-1100, 1100)/100.0f;
            float y = random.Next(-389, 483)/100.0f;
            GameObject shopPrefab = Resources.Load<GameObject>("Shop");
            Instantiate(shopPrefab, new Vector3(x, y, 0), Quaternion.identity);
            shopDisplay.GetComponent<ShopManager>().generateShopItems(ref random);
        }
    }

    public int GetSeed(){
        return seed;
    }
}
