using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedPack : MonoBehaviour
{
    public int healtPointsRefillAmount = 10;
    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (collision.gameObject.tag == "Player" && health != null)
        {
            if (health.currentHealth < health.maxHealth)
            {
                health.Heal(healtPointsRefillAmount);
                Destroy(this.gameObject);
            }
        }
    }
}
