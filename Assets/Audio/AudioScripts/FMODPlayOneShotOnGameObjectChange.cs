using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlayOneShotOnGameObjectChange : MonoBehaviour
{
    GameObject currentSelected;
    int currentInstanceID = 0;
    public FMODUnity.EventReference fmodEvent;

    void Update()
    {
        currentSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (currentSelected.GetInstanceID() != currentInstanceID)
        {
            FMODUnity.RuntimeManager.PlayOneShot(fmodEvent, transform.position);
            currentInstanceID = currentSelected.GetInstanceID();
        }

    }
}
