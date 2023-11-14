using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaneAttack : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed = 3f;
    [SerializeField] float sightRange = 0.5f;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] LayerMask whatIsPlayer;
    bool playerInSight = false;
    bool flag = false;

    private void Update()
    {
        if (!flag) playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        
        if (flag || playerInSight)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
            transform.forward = (player.position - transform.position) * -1;
            flag = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosion.Play();
        if (collision.collider.CompareTag("Player")) explosion.Play();
    }
}
