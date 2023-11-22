using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SneakyShipAttack : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    private Vector3 explosionOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] GameObject particleExplosion;
    [SerializeField] int damage = 2;

    [SerializeField] GameObject ExplosionAudio;
    private AudioPlay explosionSound;

    [Header("Patrolling")]
    public float patrolSpeed = 5f;
    public bool isPatrolling;
    public GameObject pointA;
    public GameObject pointB;
    bool whereGo = false;
    public bool isChasing = false;

    public float sightRange;
    bool playerInSightRange;
    private Animator idleAnim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        explosionSound = ExplosionAudio.GetComponent<AudioPlay>();
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
            idleAnim.enabled = false;
            if (!isChasing)
            {
                idleAnim.enabled = false;
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

        if (collision == null || collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        IHealth ihealth = collision.transform.GetComponent<IHealth>();
        if (ihealth == null)
        {
            ihealth = collision.transform.GetComponentInParent<IHealth>();
        }

        if (ihealth != null)
        {
            ihealth.ReduceHp(damage);
        }

        ShipDead();
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    public void ShipDead()
    {
        AudioPlay();
        Destroy(gameObject);
    }

    private void AudioPlay()
    {
        explosionSound.PlayAudio();
        GameObject explosion = Instantiate(particleExplosion, player.position + player.forward + explosionOffset, player.rotation);
        explosionSound.transform.parent = null;
        Destroy(explosion, 2f);
    }

    private void Patrol()
    {
        if (whereGo)
        {
            agent.SetDestination(pointA.transform.position);
            if (Vector3.Distance(pointA.transform.position, transform.position)<0.1) whereGo = false;
        }

        else
        {
            agent.SetDestination(pointB.transform.position);
            if (Vector3.Distance(pointB.transform.position, transform.position)<0.1) whereGo = true;
        }
    }

    private bool CheckPlayerPresence()
    {
        if (Physics.CheckSphere(transform.position, sightRange, whatIsPlayer))
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
