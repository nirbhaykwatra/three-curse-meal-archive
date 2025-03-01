using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crystal", menuName = "Loot/MSG Crystal")]
public class CrystalDrop : LootDrop
{
    public int _maxValue;
    
    public override void Activate(GameObject affected)
    {
        if (Resources.Crystal < _maxValue)
        {
            Resources.Crystal += 1;
        }
        
    }
}
