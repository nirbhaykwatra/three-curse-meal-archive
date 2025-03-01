using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pepper", menuName = "Loot/Pepper")]
public class PepperDrop : LootDrop
{
    public int _maxValue;
    
    public override void Activate(GameObject affected)
    {
        if (Resources.Pepper < _maxValue)
        {
            Resources.Pepper += 1;
        }
    }
}
