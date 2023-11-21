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
<<<<<<< HEAD

    [SerializeField] GameObject ExplosionAudio;
    private AudioPlay explosionSound;

    [Header("Patrolling")]
    public float patrolSpeed = 5f;
    public bool isPatrolling;
    public GameObject pointA;
    public GameObject pointB;
    bool whereGo = false;
    [HideInInspector] public bool isChasing = false;

    public float sightRange;
    private Animator idleAnim;
=======
    [SerializeField] AudioSource explosionSound;

    public float sightRange;
    public bool playerInSightRange;
>>>>>>> parent of ad503822 (Enemies done)

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
        idleAnim.enabled = false;
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
