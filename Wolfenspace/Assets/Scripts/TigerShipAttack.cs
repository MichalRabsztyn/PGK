using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TigerShipAttack : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed = 30f;
    [SerializeField] float sightRange = 0.5f;
    [SerializeField] GameObject particleExplosion;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] AudioSource explosionSound;
    [SerializeField] AudioSource StartingSound;
    Rigidbody rb;
    bool playerInSight = false;
    bool isChasing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isChasing) playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (isChasing || playerInSight)
        {
            if (!isChasing)
            {
                StartingSound.Play();
                rb.AddForce(transform.right*4f, ForceMode.VelocityChange); //small effect on spaceship starting
                isChasing = true;
            }

            ChasePlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShipDead();
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
        transform.forward = (player.position - transform.position);
    }

    private void ShipDead()
    {
        explosionSound.Play();
        GameObject explosion = Instantiate(particleExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explosion, 2f);
    }

}

