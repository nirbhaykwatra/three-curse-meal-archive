using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            player.gameObject.GetComponent<CharacterMovement3D>().SetMoveInput(new Vector3(0, 0, 0));
        }
    }
}
