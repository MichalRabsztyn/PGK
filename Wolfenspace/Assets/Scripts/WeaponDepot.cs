using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDepot : MonoBehaviour
{
    public GameObject weapon;
    private GameObject weaponInstance;
    void Start()
    {
        weaponInstance = Instantiate(weapon, this.transform.position, this.transform.rotation);
        if (weaponInstance)
        {
            weaponInstance.transform.parent = this.transform;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Equipment equipment = other.GetComponent<Equipment>();
        if (equipment != null)
        {

            GameObject weaponForPlayer = Instantiate(weapon, equipment.weaponSlot.transform.position, equipment.weaponSlot.transform.rotation);

            if (weaponForPlayer)
            {
                weaponForPlayer.transform.parent = equipment.weaponSlot.transform;
                weaponForPlayer.gameObject.SetActive(false);

                equipment.guns.Add(weaponForPlayer);
            }
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
