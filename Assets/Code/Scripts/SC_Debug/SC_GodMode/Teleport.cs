using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform _player;
    
    public void TeleportPlayer(Transform teleportPoint)
    {
        _player.GetComponent<Rigidbody>().position = teleportPoint.position;
        _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
