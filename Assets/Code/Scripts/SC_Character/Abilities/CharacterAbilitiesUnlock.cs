using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAbilitiesUnlock", menuName = "ScriptableObjects/CharacterAbilitiesUnlocks", order = 1)]
public class CharacterAbilitiesUnlock : ScriptableObject
{
    public bool broccoliRingUnlocked;

    public bool peaSMakerUnlocked;

    public bool pepperBlastUnlocked;

    public void LockAll()
    {
        broccoliRingUnlocked = false;
        peaSMakerUnlocked = false;
        pepperBlastUnlocked = false;
    }
}
