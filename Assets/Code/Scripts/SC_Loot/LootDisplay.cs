using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootDisplay : MonoBehaviour
{
    public TextMeshProUGUI _broccoliText;
    public TextMeshProUGUI _peaText;
    public TextMeshProUGUI _pepperText;

    [Required("Need access to Resources object!", InfoMessageType.Error)]
    public Resources _resources;

    private void Start()
    {
        _broccoliText.text = _resources.Broccoli.ToString();
        _peaText.text = _resources.Pea.ToString();
        _pepperText.text = _resources.Pepper.ToString();
    }

    private void Update()
    {
        _broccoliText.text = _resources.Broccoli.ToString();
        _peaText.text = _resources.Pea.ToString();
        _pepperText.text = _resources.Pepper.ToString();
    }

    public void UpdateResourcesText()
    {
        _broccoliText.text = _resources.Broccoli.ToString();
        _peaText.text = _resources.Pea.ToString();
        _pepperText.text = _resources.Pepper.ToString();
    }

    public void AddResource(string resource)
    {
        switch (resource)
        {
            case "Broccoli":
                _resources.Broccoli += 1;
                break;
            
            case "Pea":
                _resources.Pea += 1;
                break;
            
            case "Pepper":
                _resources.Pepper += 1;
                break;

        }
    }

    public void SubtractResource(string resource)
    {
        switch (resource)
        {
            case "Broccoli":
                if(_resources.Broccoli > 0) _resources.Broccoli -= 1;
                break;
            
            case "Pea":
                if(_resources.Pea > 0) _resources.Pea -= 1;
                break;
            
            case "Pepper":
                if(_resources.Pepper > 0) _resources.Pepper -= 1;
                break;

        }
    }
}
