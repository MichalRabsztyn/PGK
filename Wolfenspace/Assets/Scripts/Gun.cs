using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 1.0f;
    public int clipCapacity = 10;

    public GameObject bulletPrefab;
    public AudioClip shootSound;
    public AudioClip emptyClipSound;

    [System.NonSerialized] public AudioSource audioSource;
    private Transform bulletSpawner;
    private ParticleSystem muzzleFlash;
    [System.NonSerialized] public int bulletsInClip = 0;

    private void Start()
    {
        bulletSpawner = this.transform.Find("Bullet Spawner");
        audioSource = GameObject.Find("Audio Component").GetComponent<AudioSource>();
        bulletsInClip = clipCapacity;
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        if (bulletsInClip > 0)
        {
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
