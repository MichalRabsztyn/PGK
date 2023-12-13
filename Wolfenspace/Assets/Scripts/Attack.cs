using System.Diagnostics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [System.NonSerialized] public float nextFireTime;
    [System.NonSerialized] public GameObject weaponModel;
    [System.NonSerialized] public Weapon weapon;
    private Inventory inventory;

    void Start()
    {
           
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time >= nextFireTime)
            {
                if(weapon == null)
                {
                    return;
                }

                /*if (weapon.bulletsInClip <= 0 && weapon.audioSource != null && weapon.emptyClipSound != null)
                {
                    weapon.audioSource.PlayOneShot(weapon.emptyClipSound);
                }
                else
                {
                    Shoot();
                }  */ 
                
                

                nextFireTime = Time.time + 1f / UseWeapon();
            }
        }
    }

    float UseWeapon()
    {
        if (weapon.UseWeapon())
        {
            return weapon.usesPerSecond;
        }
        else
        {
            return 0.5f;
        }
    }
}