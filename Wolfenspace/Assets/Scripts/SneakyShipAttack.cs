using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SneakyShipAttack : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    private Vector3 explosionOffset = new Vector3(-0.8f, 1.5f, -1.5f);
    [SerializeField] GameObject particleExplosion;
    [SerializeField] AudioSource explosionSound;

    public float sightRange;
    public bool playerInSightRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (playerInSightRange) ChasePlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ShipDead();
        }
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void ShipDead()
    {
        explosionSound.Play();
        GameObject explosion = Instantiate(particleExplosion, transform.position + explosionOffset, transform.rotation);
        Destroy(gameObject);
        Destroy(explosion, 2f);
    }

}
