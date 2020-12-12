using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UniJSON;
using UnityEngine;
using UnityEngine.Analytics;

public class Enemy : MonoBehaviour
{
    // Bools
    public bool isRanged;

    // Stats
    public int health;
    public int damage;
    public float speed;
    public float steer;
    public float detectRadius;
    private Vector3 emptyVector;

    // References
    public LayerMask playerLayer;
    public WeaponManager wm;
    private Transform enemyTransform;
    private Vector3 playerPos;

    // States
    private bool playerDetected;

    private Inventory inventory;

    private void Awake()
    {
        enemyTransform = gameObject.transform;
        emptyVector = new Vector3(0, 0, 0);
    }

    private void OnDeath()
    {
        Item item = new Item {itemType = Item.ItemType.Meat, amount = 20};
        ItemWorld.SpawnItemWorld(transform.position, item);
        Dictionary<String, int>progress = AddToSaveGame(item);
        SaveGame(progress);
        gameObject.SetActive(false);
    
    }

    /// <summary>
    /// Tries to find the player in a radius around the enemy, returns the position of the player if found.
    /// </summary>
    /// <returns>Vector3 containing player position or position 0 if not found</returns>
    private Vector3 DetectPlayer()
    {
        Collider[] colldiers = Physics.OverlapSphere(enemyTransform.position, detectRadius, playerLayer);

        if (colldiers.Length > 0)
        {
            foreach (var target in colldiers)
            {
                if (target.CompareTag("Player"))
                {
                    return target.transform.position;
                }
            }
        }

        return emptyVector;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }

    private void AIManager()
    {
        playerPos = DetectPlayer();

        if (playerPos != emptyVector)
        {
            Vector3 lookVector = playerPos - transform.position;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1f);

            if (isRanged)
            {
            }
            else
            {
                gameObject.transform.position += transform.forward * Time.deltaTime * speed;
            }
        }
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            target.gameObject.GetComponent<Submarine>().TakeDamage(damage);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            AIManager();
        }
        else
        {
            OnDeath();
        }
    }

    public void SaveGame(Dictionary<String, int> values)
    {
        string docupath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
        string json = values.ToString();

        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docupath, "indepth.save")))
        {
            outputFile.WriteLine(json);
        }
    }

    public Dictionary<String, int> AddToSaveGame(Item item)
    {
        string savegame;
        string docupath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        using (StreamReader inputFile = new StreamReader(Path.Combine(docupath, "indepth.save")))
        {
            savegame = inputFile.ReadLine();
            var values = JsonConvert.DeserializeObject<Dictionary<String, int>>(savegame);
            if (values.ContainsKey(item.itemType.ToString()))
            {
                values[item.itemType.ToString()] += item.amount;
            }
            else
            {
                values[item.itemType.ToString()] = item.amount;
            }

            return values;
        }
    }

    public void SaveGame()
    {
        string docupath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var dict = new JsonFormatter();
        dict.BeginMap();
        foreach (Item item in inventory.GetItemList())
        {
            dict.Key(item.itemType.ToString());
            dict.Value(item.amount);
        }

        dict.EndMap();

        string json = dict.ToString();

        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docupath, "indepth.save")))
        {
            outputFile.WriteLine(json);
        }
    }
}