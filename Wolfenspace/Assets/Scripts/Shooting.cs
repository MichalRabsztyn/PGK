using System.Diagnostics;
using UnityEngine;

public class Shooting : MonoBehaviour
{  
    public GameObject weaponPrefab;
    [System.NonSerialized] public float nextFireTime;

    [System.NonSerialized] public Gun gun;

    void Start()
    {
        GameObject weaponSlot = GameObject.Find("First Person Controller/First Person Camera/Weapon Slot");
        if (weaponSlot)
        {
            GameObject weapon = Instantiate(weaponPrefab, weaponSlot.transform.position, Quaternion.identity);
            if (weapon)
            {
                weapon.transform.parent = weaponSlot.transform;
                
                gun = weapon.GetComponent<Gun>();
            }
        }      
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && gun != null)
        {
            if (Time.time >= nextFireTime)
            {
                if (gun.bulletsInClip <= 0)
                {
                    gun.audioSource.PlayOneShot(gun.emptyClipSound);
                }
                else
                {
                    Shoot();
                }   
                
                nextFireTime = Time.time + 1f / gun.fireRate;
            }
        }
    }

    void Shoot()
    {
        gun.Shoot();
    }
}