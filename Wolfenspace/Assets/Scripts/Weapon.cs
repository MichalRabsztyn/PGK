using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int usesPerSecond = 1;
    public int weaponID = 0;
    public GameObject weaponModel;

    public virtual bool UseWeapon() { return false; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
