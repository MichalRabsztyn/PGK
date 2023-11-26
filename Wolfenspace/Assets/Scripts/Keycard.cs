using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Equipment equipment = other.GetComponent<Equipment>();
        if (equipment != null)
        {
            equipment.numberOfKeys++;
            
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
