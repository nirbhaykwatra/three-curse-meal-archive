using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesUICheck : MonoBehaviour
{
    [SerializeField] CharacterAbilitiesUnlock _characterAbilitiesUnlockParams;
    [SerializeField] Resources _resources;
    [SerializeField] GameObject _buttonBroccoli;
    [SerializeField] GameObject _buttonPeaSMaker;
    [SerializeField] GameObject _buttonPepper;
    string trigger;

    private void Start()
    {
        //Makes abilities buttons go to disabled state
        _buttonBroccoli.GetComponent<Button>().interactable = false;
        _buttonPeaSMaker.GetComponent<Button>().interactable = false;
        _buttonPepper.GetComponent<Button>().interactable = false;
    }

    public void UpdateButtonState()
    {
        if(_characterAbilitiesUnlockParams.pepperBlastUnlocked)
        {
            if (_resources.Pepper < 2 || _resources.Broccoli < 1) trigger = "Highlighted";
            if (_resources.Pepper >= 2 && _resources.Broccoli >= 1) trigger = "Normal";
            _buttonPepper.GetComponent<Animator>().SetTrigger(trigger);
        }
        if (_characterAbilitiesUnlockParams.peaSMakerUnlocked)
        {
            if (_resources.Pea < 2 || _resources.Pepper < 1) trigger = "Highlighted";
            if (_resources.Pea >= 2 && _resources.Pepper >= 1) trigger = "Normal";
            _buttonPeaSMaker.GetComponent<Animator>().SetTrigger(trigger);
        }
        if (_characterAbilitiesUnlockParams.broccoliRingUnlocked)
        {
            if (_resources.Broccoli < 4 || _resources.Pea < 1) trigger = "Highlighted";
            if (_resources.Broccoli >= 4 && _resources.Pea >= 1) trigger = "Normal";
            _buttonBroccoli.GetComponent<Animator>().SetTrigger(trigger);
        }

    }
    public IEnumerator DashTimer()
    {
        float time = 0f;
        while (time <3f)
        {
            time += Time.deltaTime;
        }
        yield return null;
    }
}

