using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Broccoli", menuName = "Loot/Broccoli")]
public class BroccoliDrop : LootDrop
{
    public int _maxValue;
    
    public override void Activate(GameObject affected)
    {
        if (Resources.Broccoli < _maxValue)
        {
            Resources.Broccoli += 1;
        }
    }
}
