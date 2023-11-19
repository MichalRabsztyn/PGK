using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoRefillAmount = 10; 
    private void OnCollisionEnter(Collision collision)
    {
        Shooting shooting = collision.gameObject.GetComponent<Shooting>();
        if (collision.gameObject.tag == "Player" && shooting != null && shooting.gun != null)
        {
            if (shooting.gun.bulletsInClip < shooting.gun.clipCapacity)
            {
                shooting.gun.RefillAmmo(ammoRefillAmount);
                Destroy(this.gameObject);
            }
        }
    }
}
