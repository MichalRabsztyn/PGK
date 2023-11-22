using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1.0f;
    public float damage = 1.0f;
    public float significanceDistance = 100.0f;
    public LayerMask layerMask;

    [HideInInspector] public Vector3 movementDirection = Vector3.forward;
    private Vector3 originalLocation = Vector3.zero;

    void Start()
    {
        originalLocation = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Vector3 bulletDirection = hit.point - originalLocation;
            this.GetComponent<Rigidbody>().velocity = bulletDirection * speed;
        }
        else
        {
            Vector3 bulletDirection = (Camera.main.transform.position + Camera.main.transform.forward * 1000) - originalLocation;
            this.GetComponent<Rigidbody>().velocity = bulletDirection * speed;
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, originalLocation) > significanceDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null || collision.gameObject.CompareTag("Player"))
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

        Destroy(this.gameObject);
    }
}
