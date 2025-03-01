using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float spinforce = 10f;
    [SerializeField] private bool isTimeDestroyed = false;
    [SerializeField] private float maxSpreadAngle = 1f;
    [SerializeField] private float timedDestruction = 10;
    private Rigidbody rb;
    private float randomAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //shoot projectile forward
        randomAngle = Random.Range(0.6f,maxSpreadAngle);
        Vector3 rng = new Vector3(transform.forward.x * randomAngle, transform.forward.y * randomAngle, transform.forward.z * randomAngle);
        rb.velocity = rng * speed;
        //adss a rotation to the projectile
        rb.AddTorque(Random.insideUnitSphere * spinforce);
        if (isTimeDestroyed)
        {
            Destroy(gameObject, timedDestruction);
        }
    }
}
