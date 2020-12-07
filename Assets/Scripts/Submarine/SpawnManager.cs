using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Maximum amount of entities at one time
    public int maxEnemies = 20;
    public int maxBoids = 4;
    public int maxVegetation = 200;
    
    //Positional variables
    public int spawnHeight = 50;     // Height at which ray is cast downwards to see if an enemy can spawn
    public int maxRayDist = 500;     // Raycast maximum vertical distance
    public int spawnEntityRadious = 10;    // Radious arround the submarine where enemies cann spawn
    public int spawnVegetationRadious = 50;    // Radious arround the submarine where enemies cann spawn
    private RaycastHit rayHit;       // Ray has struck ground here.
    private float rndX, rndZ;        // Random positions where ray can spawn
    private Vector3 rayLocation;    // Ray is cast here
    public LayerMask groundLayer;    // Layer where raycast can hit
    private Vector3 curpos;        //Position of the submarine, avoids repeated call
    
    //Score trackers
    public int winAmmount = 20;
    public int scoreAmmount = 0;
    
    //GameObjects
    public GameObject boidSpawner;
    public GameObject enemyMelee;
    public GameObject enemyRanged;

    //GameObject trackers
    private List<GameObject> currentEnemies;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentEnemies = new List<GameObject>();
    }

    void SpawnVegetation()
    {
        
    }
    
    void SpawnBoids()
    {
        
    }
    
    void SpawnEnemies()
    {
        curpos = transform.position;
        
        rndX = Random.Range(curpos.x - spawnEntityRadious, curpos.x + spawnEntityRadious);
        rndZ = Random.Range(curpos.z - spawnEntityRadious, curpos.z + spawnEntityRadious);
        rayLocation = new Vector3(rndX, spawnHeight, rndZ);
        
        if (Physics.Raycast(rayLocation, Vector3.down, out rayHit, maxRayDist, groundLayer))
        {
            Vector3 elveatePos = new Vector3(rayHit.point.x, rayHit.point.y + 5, rayHit.point.z);
            GameObject currEnemy = Instantiate(enemyMelee, elveatePos, transform.rotation);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemies.Count < maxEnemies)
        {
            SpawnEnemies();
        }
    }

    public void IncreaseScore()
    {
        scoreAmmount++;
    }
}
