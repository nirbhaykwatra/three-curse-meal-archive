using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthDrop", menuName = "Loot/Health")]
public class HealthDrop : LootDrop
{
    public override void Activate(GameObject affected)
    {
        affected.GetComponent<Health>().Heal(20f);
    }
}