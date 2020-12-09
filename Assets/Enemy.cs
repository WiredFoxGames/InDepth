using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private void Awake()
    {
        enemyTransform = gameObject.transform;
        emptyVector = new Vector3(0,0,0);
    }

    private void OnDeath()
    {
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
}
