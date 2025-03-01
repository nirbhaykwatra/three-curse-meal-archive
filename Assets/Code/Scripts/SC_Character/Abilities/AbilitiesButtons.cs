using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesButtons : MonoBehaviour
{
    [SerializeField] CharacterAbilitiesUnlock _characterAbilitiesUnlockParams;
    [SerializeField] Resources _resources;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnPeaSMaker()
    {
        if (_characterAbilitiesUnlockParams.peaSMakerUnlocked && _resources.Pepper > 0 && _resources.Pea > 1)
            _animator.SetTrigger("Pressed");
    }
    public void OnPepperBlast()
    {
        if (_characterAbilitiesUnlockParams.pepperBlastUnlocked && _resources.Pepper > 1 && _resources.Broccoli > 0)
            _animator.SetTrigger("Pressed");
    }
    public void OnBroccoliRing()
    {
        if (_characterAbilitiesUnlockParams.broccoliRingUnlocked && _resources.Broccoli > 3 && _resources.Pea > 0)
            _animator.SetTrigger("Pressed");
    }
}
