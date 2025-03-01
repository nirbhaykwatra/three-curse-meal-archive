using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pea", menuName = "Loot/Pea")]
public class PeaDrop : LootDrop
{
    public int _maxValue;
    
    public override void Activate(GameObject affected)
    {
        if (Resources.Pea < _maxValue)
        {
            Resources.Pea += 1;
        }
        
    }
}
