using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public void DestroyFloor()
    {
        if (gameObject == null) return;
        Destroy(gameObject);
    }
}
