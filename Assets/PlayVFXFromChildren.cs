using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayVFXFromChildren : MonoBehaviour
{
    RockVFXEmpty[] childrenVFXSytems;

    private void Start()
    {
        childrenVFXSytems = gameObject.GetComponentsInChildren<RockVFXEmpty>();
    }

    public void PlayVFX()
    {
        foreach (RockVFXEmpty childVFX in childrenVFXSytems)
        {
            childVFX.GetComponent<VisualEffect>().SendEvent("OnPlay");

        }
    }
}
