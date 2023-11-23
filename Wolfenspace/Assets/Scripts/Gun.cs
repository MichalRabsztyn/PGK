using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int weaponID = 0;
    public float fireRate = 1.0f;
    public int clipCapacity = 10;

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
    }

    public void Shoot()
    {
        if(!bulletSpawner) 
        { 
            return;
        }
        if(!bulletPrefab)
        {
            return;
        } 
        if(!emptyClipSound) 
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
}
