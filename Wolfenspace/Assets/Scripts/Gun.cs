using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : Weapon
{
    public float fireRate = 1.0f;
    public int clipCapacity = 10;
    public int clipsize = 10;

    public GameObject gunHUD;
    public GameObject bulletPrefab;
    public AudioClip shootSound;
    public AudioClip emptyClipSound;

    [System.NonSerialized] public AudioSource audioSource;
    private Transform bulletSpawner;
    private ParticleSystem muzzleFlash;
    [System.NonSerialized] public int bulletsInClip = 0;

    private void Awake()
    {
        bulletSpawner = this.transform.Find("Bullet Spawner");
        audioSource = GetComponent<AudioSource>();
        bulletsInClip = clipCapacity;

        if (gunHUD)
        {
            gunHUD.GetComponent<Text>().text = bulletsInClip.ToString();
        }
    }

    public void Shoot()
    {
        if (!bulletSpawner)
        {
            return;
        }
        if (!bulletPrefab)
        {
            return;
        }
        if (!emptyClipSound)
        {
            return;
        }

        if (bulletsInClip > 0)
        {
            Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);

            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }

            bulletsInClip--;

            if (gunHUD)
            {
                gunHUD.GetComponent<Text>().text = bulletsInClip.ToString();
            }
        }
    }

    public void RefillAmmo(int amount)
    {
        if (bulletsInClip + amount >= clipCapacity)
        {
            bulletsInClip = clipCapacity;
        }
        else
        {
            bulletsInClip = bulletsInClip + amount;
        }
    }

    override public bool UseWeapon() 
    {
        Shoot();

        return true;
    }
}
