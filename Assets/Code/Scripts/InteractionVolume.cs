using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionVolume : MonoBehaviour
{
    public List<Chest> _chests = new List<Chest>();
    public List<Altar> _altars;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Altar altar))
        {
            _altars.Add(altar);
        }
        
        if (other.TryGetComponent(out Chest chest))
        {
            _chests.Add(chest);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_chests.Contains(other.gameObject.GetComponent<Chest>()))
        {
            _chests.Remove(other.gameObject.GetComponent<Chest>());
        }
        
        if (_altars.Contains(other.gameObject.GetComponent<Altar>()))
        {
            _altars.Remove(other.gameObject.GetComponent<Altar>());
        }
    }
}
