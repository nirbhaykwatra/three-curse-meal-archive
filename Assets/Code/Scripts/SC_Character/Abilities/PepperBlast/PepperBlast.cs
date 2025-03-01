using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PepperBlast", menuName = "Abilities/Pepper Blast")]
public class PepperBlast : ScriptableObject
{
    public float BlastRadius;
    public float BlastDamage;
    public float BlastForce;
    public float PropBlastForce;
    public LayerMask EnemyLayer;
    public LayerMask PropsLayer;
    
    public void PepperBlastAbility(Vector3 center, GameObject self)
    {
        Collider[] enemies = Physics.OverlapSphere(center, BlastRadius, EnemyLayer);
        Collider[] props = Physics.OverlapSphere(center, BlastRadius, PropsLayer);

        foreach (Collider hit in enemies)
        {
            /* if (hit.TryGetComponent(out PlayerController player))
            {
                continue;
            }*/
            if (hit.GetComponent<Rigidbody>() != null)
            {
                hit.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Vector3 direction = hit.transform.position - self.GetComponent<Rigidbody>().position;
                hit.GetComponent<Rigidbody>().AddForce(direction * BlastForce, ForceMode.Impulse);
            }
            if (hit.TryGetComponent(out Health health))
            {
                hit.GetComponent<Health>().Damage(BlastDamage, self);
                Debug.Log("dealt damage");
            }
        }
        
        foreach (Collider hit in props)
        {
            if (hit.TryGetComponent(out PlayerController player))
            {
                continue;
            }

            hit.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 direction = hit.transform.position - self.GetComponent<Rigidbody>().position;
            hit.GetComponent<Rigidbody>().AddForce(direction * BlastForce, ForceMode.Impulse);

            if (hit.TryGetComponent(out Destructible destructible))
            {
                hit.GetComponent<Destructible>().Blast(direction);
            }
        }
    }
}
