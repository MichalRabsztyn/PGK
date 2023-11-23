using System.Diagnostics;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [System.NonSerialized] public float nextFireTime;
    [System.NonSerialized] public GameObject weapon;
    [System.NonSerialized] public Gun gun;
    private Equipment equipment;

    void Start()
    {
        GameObject weaponSlot = GameObject.Find("First Person Controller/First Person Camera/Weapon Slot");
        if (weaponSlot)
        {
            equipment = GetComponent<Equipment>();
            if (equipment && equipment.guns.Length >= 0)
            {
                weapon = Instantiate(equipment.guns[0], weaponSlot.transform.position, weaponSlot.transform.rotation);
                if (weapon)
                {
                    gun = weapon.GetComponent<Gun>();
                    weapon.transform.parent = weaponSlot.transform;
                }
            }
        }     
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time >= nextFireTime)
            {
                if(gun == null)
                {
                    return;
                }

                if (gun.bulletsInClip <= 0 && gun.audioSource != null && gun.emptyClipSound != null)
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