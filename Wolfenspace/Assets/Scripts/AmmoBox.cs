using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoRefillAmount = 10; 
    private void OnCollisionEnter(Collision collision)
    {
        Attack attack = collision.gameObject.GetComponent<Attack>();
        if (collision.gameObject.tag == "Player" && attack != null && attack.weapon != null)
        {
            //if (attack.weapon.bulletsInClip < attack.weapon.clipCapacity)
            {
                //attack.weapon.RefillAmmo(ammoRefillAmount);
                Destroy(this.gameObject);
            }
        }
    }
}
