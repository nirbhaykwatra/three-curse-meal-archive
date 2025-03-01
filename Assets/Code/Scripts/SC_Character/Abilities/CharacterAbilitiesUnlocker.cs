using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterAbilitiesUnlocker : MonoBehaviour
{
    [SerializeField] private CharacterAbilitiesUnlock _characterAbilitiesUnlockParams;

    public void UnlockBroccoliRing()
    {
        _characterAbilitiesUnlockParams.broccoliRingUnlocked = true;
    }

    public void UnlockPeaSMaker()
    {
        _characterAbilitiesUnlockParams.peaSMakerUnlocked = true;
    }

    public void UnlockPepperBlast()
    {
        _characterAbilitiesUnlockParams.pepperBlastUnlocked = true;
    }

}
