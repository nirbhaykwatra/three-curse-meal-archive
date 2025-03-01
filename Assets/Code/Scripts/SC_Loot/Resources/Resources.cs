using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Resources", menuName = "Loot/Resource Counts")]
public class Resources : ScriptableObject
{
    //private fields
    private int broccoliDeath;
    private int peaDeath;
    private int pepperDeath;
    private int crystalsDeath;
    
    //public fields
    [ShowInInspector]
    private int broccoli;
    [ShowInInspector]
    private int pea;
    [ShowInInspector]
    private int pepper;
    [ShowInInspector]
    private int crystals;

    //public properties
    public int Broccoli { get => broccoli; set => broccoli = value; }
    public int Pea { get => pea; set => pea = value; }
    public int Pepper { get => pepper; set => pepper = value; }
    public int Crystal {get => crystals; set => crystals = value; }

    [Button(ButtonSizes.Large)]
    public void ResetResources()
    {
        broccoli = 0;
        pea = 0;
        pepper = 0;
        crystals = 0;
    }

    public void StoreResources()
    {
        broccoliDeath = broccoli;
        peaDeath = pea;
        pepperDeath = pepper;
        crystalsDeath = crystals;
    }

    public void ResetResourcesOnRespawn()
    {
        broccoli = broccoliDeath;
        pea = peaDeath;
        pepper = pepperDeath;
        crystals = crystalsDeath;
    }
}
