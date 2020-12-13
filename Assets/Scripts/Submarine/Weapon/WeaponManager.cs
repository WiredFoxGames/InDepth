using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public GameObject bullet;


    public void LoadNewGun(int i)
    {
        switch (i)
        {
            case 0:
                damage = 60;
                timeBetweenShooting = 0.05f;
                spread = 2f;
                range = 50f;
                reloadTime = 0f;
                timeBetweenShooting = 0.0001f;
                magazineSize = 50000000;
                bulletsPerTap = 2;
                allowButtonHold = true;
                break;
            
            case 1:
                damage = 20000;
                timeBetweenShooting = 0.1f;
                spread = 0f;
                range = 200f;
                reloadTime = 0f;
                timeBetweenShooting = 0.1f;
                magazineSize = 50000000;
                bulletsPerTap = 1;
                allowButtonHold = true;
                break;
            
            case 2:
                damage = 20;
                timeBetweenShooting = 0.005f;
                spread = 4f;
                range = 20f;
                reloadTime = 0f;
                timeBetweenShooting = 0.001f;
                magazineSize = 50000000;
                bulletsPerTap = 20;
                allowButtonHold = true;
                break;
            
            case 3:
                damage = 2000;
                timeBetweenShooting = 0.005f;
                spread = 4f;
                range = 20f;
                reloadTime = 0f;
                timeBetweenShooting = 0.001f;
                magazineSize = 50000000;
                bulletsPerTap = 80;
                allowButtonHold = true;
                break;
        }
    }

    public WeaponManager(int damage, float timeBetweenShooting, float spread, float range, float reloadTime, float timeBetweenShots, int magazineSize, int bulletsPerTap, bool allowButtonHold)
    {
        this.damage = damage;
        this.timeBetweenShooting = timeBetweenShooting;
        this.spread = spread;
        this.range = range;
        this.reloadTime = reloadTime;
        this.timeBetweenShots = timeBetweenShots;
        this.magazineSize = magazineSize;
        this.bulletsPerTap = bulletsPerTap;
        this.allowButtonHold = allowButtonHold;
        this.bullet = this.bullet;
    }

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Instantiate bullet
        Vector3 rndSpread = new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread),
            Random.Range(-spread, spread));
        GameObject movingBullet;
        movingBullet = Instantiate(bullet, transform.position, transform.rotation);
        movingBullet.transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(transform.forward + rndSpread), 1f * Time.deltaTime);
        movingBullet.GetComponent<BulletConf>().damage = damage;
        movingBullet.GetComponent<BulletConf>().rotated = true;

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}