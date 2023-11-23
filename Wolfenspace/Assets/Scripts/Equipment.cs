using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public GameObject[] guns;
    public int numberOfKeys = 0;
    public int currentEquipmentSlot = 0;
    public float weaponChangeTime = 1.0f;

    private float nextChangeTime;
    private Shooting shooting;
    private GameObject weaponSlot;

    public void Start()
    {
        shooting = GetComponent<Shooting>();
        weaponSlot = GameObject.Find("First Person Controller/First Person Camera/Weapon Slot");
    }
    public void Update()
    {
        if (guns.Length >= 2)
        {
            if (Time.time >= nextChangeTime)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    currentEquipmentSlot++;
                    if (currentEquipmentSlot >= guns.Length)
                    {
                        currentEquipmentSlot = 0;
                    }

                    ChangeWeaponSlot(currentEquipmentSlot);
                    nextChangeTime = Time.time + weaponChangeTime;
                }
                else if (Input.GetKey(KeyCode.Q))
                {
                    currentEquipmentSlot--;
                    if (currentEquipmentSlot < 0)
                    {
                        currentEquipmentSlot = guns.Length - 1;
                    }

                    ChangeWeaponSlot(currentEquipmentSlot);
                    nextChangeTime = Time.time + weaponChangeTime;
                }            
            }
        }
    }

    public void ChangeWeaponSlot(int weaponIndex)
    {
        if (shooting)
        {
            if (weaponSlot)
            {
                GameObject weapon = Instantiate(guns[weaponIndex].gameObject, weaponSlot.transform.position, weaponSlot.transform.rotation);
                if (weapon)
                {
                    foreach (Transform child in weaponSlot.transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                    shooting.weapon = guns[weaponIndex].gameObject;
                    shooting.gun = weapon.GetComponent<Gun>();

                    weapon.transform.parent = weaponSlot.transform;
                }

            }
        }
    }
}
