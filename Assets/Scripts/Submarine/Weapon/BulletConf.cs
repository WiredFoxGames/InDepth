using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletConf : MonoBehaviour
{
    public int lifespan = 5;
    public bool rotated = false;

    void Update()
    {
        if (lifespan > 0)
        {
            if (rotated)
            {
                transform.position += transform.forward * 20 * Time.deltaTime;
                lifespan--;
            }
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
    }
}


