using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TigerShipAttack : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float atackSpeed = 30f;
    [SerializeField] float sightRange = 0.5f;
    [SerializeField] GameObject particleExplosion;
    [SerializeField] LayerMask whatIsPlayer;

    [Header("Sounds")]
    [SerializeField] GameObject ExplosionAudio;
    private AudioPlay explosionSound;

    [SerializeField] GameObject StartAudio;
    private AudioPlay startSound;

    Rigidbody rb;
    public bool isChasing = false;

    [Header("Patrolling")]
    public float patrolSpeed = 10f;
    public bool isPatrolling;
    public GameObject pointA;
    public GameObject pointB;
    private bool whereGo = false;

    private Animator idleAnim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        explosionSound = ExplosionAudio.GetComponent<AudioPlay>();
        startSound = StartAudio.GetComponent<AudioPlay>();
        pointA.transform.parent = null;
        pointB.transform.parent = null;
        idleAnim = GetComponentInChildren<Animator>();
    }
    
    private void Update()
    {
        if (isPatrolling)
        {
            idleAnim.enabled = false;
            Patrol();
        }

        if (isChasing || CheckPlayerPresence())
        {          
            if (!isChasing) //code in this if executes only once
            {
                idleAnim.enabled = false;
                startSound.PlayAudio();
                rb.AddForce(transform.right*4f, ForceMode.VelocityChange); //small effect on spaceship starting
                isChasing = true;

                pointA.GetComponent<DestroyMyself>().Destroy();
                pointB.GetComponent<DestroyMyself>().Destroy();
                isPatrolling = false;
            }

            ChasePlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            ShipDead();
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, atackSpeed*Time.deltaTime);
        transform.forward = (player.position - transform.position);
    }

    public void ShipDead()
    {
        explosionSound.PlayAudio();
        particleExplode();
        Destroy(gameObject);
    }

    private void particleExplode()
    {
        GameObject explosion = Instantiate(particleExplosion, transform.position, transform.rotation);
        Destroy(explosion, 2f);
    }

    private void Patrol()
    {
        if(whereGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.transform.position, patrolSpeed*Time.deltaTime);
            transform.forward = (pointA.transform.position - transform.position);
            if (transform.position == pointA.transform.position) whereGo = false;
        }
            
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.transform.position, patrolSpeed*Time.deltaTime);
            transform.forward = (pointB.transform.position - transform.position);
            if (transform.position == pointB.transform.position) whereGo = true;
        }
    }

    private bool CheckPlayerPresence()
    {
        if(Physics.CheckSphere(transform.position, sightRange, whatIsPlayer))
        {
            Vector3 direction = player.position - transform.position;

            Ray ray = new Ray(transform.position, direction);

            float maxDistance = direction.magnitude - 0.5f;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, 7)) return false;
            else return true;
        }

        return false;
    }

}

