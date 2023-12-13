using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDepot : MonoBehaviour
{
    public Weapon weaponPrefab;
    public bool bRespawnWeapon = false;
    public float weaponRespawnDelay = 5.0f;

    private GameObject weaponInstance;
    void Start()
    {
        if (weaponPrefab == null) 
        { 
            return;
        }

        weaponInstance = Instantiate(weaponPrefab.gameObject, this.transform.position, this.transform.rotation);
        if (weaponInstance)
        {
            weaponInstance.transform.parent = this.transform;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Inventory equipment = other.GetComponent<Inventory>();
        if (equipment)
        {
            if (weaponPrefab)
            {
                foreach (Weapon weapon in equipment.weapons)
                {
                    if (weapon.weaponID == weaponPrefab.weaponID)
                    {
                        return;
                    }
                }

                GameObject weaponForPlayer = Instantiate(weaponPrefab.gameObject, equipment.weaponSlot.transform.position, equipment.weaponSlot.transform.rotation);

                if (weaponForPlayer)
                {
                    weaponForPlayer.transform.parent = equipment.weaponSlot.transform;
                    weaponForPlayer.gameObject.SetActive(false);

                    equipment.weapons.Add(weaponForPlayer.GetComponent<Weapon>());
                }

                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                if (bRespawnWeapon)
                {
                    Invoke("RespawnWeapon", weaponRespawnDelay);
                }
            }
        }
    }

    void RespawnWeapon()
    {
        if (weaponPrefab == null)
        {
            return;
        }

        weaponInstance = Instantiate(weaponPrefab.gameObject, this.transform.position, this.transform.rotation);
        if (weaponInstance)
        {
            weaponInstance.transform.parent = this.transform;
        }
    }
}
