using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    //Maximum amount of entities at one time
    public int maxEnemies = 20;
    public int maxBoids = 4;
    public int maxVegetation = 200;
    
    //Positional variables
    public int spawnHeight = 50;     // Height at which ray is cast downwards to see if an enemy can spawn
    public int maxRayDist = 1000;     // Raycast maximum vertical distance
    public int spawnEntityRadious = 10;    // Radious arround the submarine where enemies cann spawn
    public int spawnVegetationRadious = 50;    // Radious arround the submarine where enemies cann spawn
    private RaycastHit rayHit;       // Ray has struck ground here.
    private float rndX, rndZ;        // Random positions where ray can spawn
    private float rndScale;        // Random scale, used for plants
    private Vector3 rayLocation;    // Ray is cast here
    public LayerMask groundLayer;    // Layer where raycast can hit
    private Vector3 curpos;        //Position of the submarine, avoids repeated call
    
    //Score trackers
    public int winAmmount = 20;
    public int scoreAmmount = 0;
    
    //GameObjects
    public GameObject plantSpawner;
    public GameObject boidSpawner;
    public List<GameObject> enemyMelee;
    public GameObject enemyRanged;

    //GameObject trackers
    private List<GameObject> currentEnemies;
    private List<GameObject> currentVegetation;
    
    //Bools
    private bool readyToSpawn = false;
    

    void SpawnVegetation()
    {
        curpos = transform.position;
        Random.seed = Random.seed + 2;
        rndScale = Random.Range(0.2f, 12f);
        float rndHeight = Random.Range(0.2f, 16f);
        
        rndX = Random.Range(curpos.x - spawnVegetationRadious, curpos.x + spawnVegetationRadious);
        rndZ = Random.Range(curpos.z - spawnVegetationRadious, curpos.z + spawnVegetationRadious);

        rayLocation = new Vector3(rndX, spawnHeight, rndZ);
        
        if (Physics.Raycast(rayLocation, Vector3.down, out rayHit, maxRayDist, groundLayer))
        {
            GameObject currPlant = Instantiate(plantSpawner, rayHit.point, transform.rotation);
            currPlant.transform.localScale = new Vector3(rndScale, rndHeight, rndScale);
            currPlant.transform.rotation = Quaternion.LookRotation(Vector3.left);
            currentVegetation.Add(currPlant);
        }
    }
    
    void SpawnBoids()
    {
        
    }
    
    void SpawnEnemies()
    {
        int rndEnemy = Random.Range(0, enemyMelee.Count);
        curpos = transform.position;
        
        rndX = Random.Range(curpos.x - spawnEntityRadious, curpos.x + spawnEntityRadious);
        rndZ = Random.Range(curpos.z - spawnEntityRadious, curpos.z + spawnEntityRadious);
        rayLocation = new Vector3(rndX, spawnHeight, rndZ);
        
        if (Physics.Raycast(rayLocation, Vector3.down, out rayHit, maxRayDist, groundLayer))
        {
            Vector3 elveatePos = new Vector3(rayHit.point.x, rayHit.point.y + 5, rayHit.point.z);
            GameObject currEnemy = Instantiate(enemyMelee[rndEnemy], elveatePos, transform.rotation);
            currentEnemies.Add(currEnemy);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToSpawn)
        {
            while (currentVegetation.Count < maxVegetation)
            {
                SpawnVegetation();
            }
            
            if (currentEnemies.Count < maxEnemies)
            {
                SpawnEnemies();
            }
        }
        else
        {
            if (gameObject.GetComponent<MeshGenerator>().LoadedStatus())
            {
                Debug.Log("Ready to spawn");
                currentEnemies = new List<GameObject>();
                currentVegetation = new List<GameObject>();

                readyToSpawn = true;
            }
        }
    }

    
    
    public void IncreaseScore()
    {
        scoreAmmount++;
    }
    
}
