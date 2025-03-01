using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooterEmission : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer _peaShooterMaterial;
    [SerializeField] [ColorUsage(true, true)] Color _emissionColor;
    public void StopEmission()
    {
        _peaShooterMaterial.material.SetColor("_EmissionColor", new Color(0, 0, 0));
    }
    public void ResetEmission()
    {
        _peaShooterMaterial.material.SetColor("_EmissionColor", _emissionColor);
    }
    public void StartAttackEmission()
    {
        _peaShooterMaterial.material.SetColor("_EmissionColor", new Color(255, 0, 0));
    }
}
