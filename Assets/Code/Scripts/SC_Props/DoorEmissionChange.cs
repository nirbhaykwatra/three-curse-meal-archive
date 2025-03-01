using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEmissionChange : MonoBehaviour
{
    [SerializeField] MeshRenderer _doorMaterial;
    [SerializeField] [ColorUsage(true, true)] Color _encounterColor;
    [SerializeField] float _lerpDuration;
    private Color _defaultColor;
    private void Awake()
    {
        _defaultColor = _doorMaterial.material.GetColor("_EmissionColor");
    }
    public void EncounterEmission()
    {
        StartCoroutine(EncounterEmissionLerp());
    }
    public void ResetEmission()
    {
        StartCoroutine(ResetEmissionLerp());
    }
    
    private IEnumerator EncounterEmissionLerp()
    {
        float t = 0;
        Color lerpedColor;
        while (t < _lerpDuration)
        {
            lerpedColor = Color.Lerp(_defaultColor, _encounterColor, t/_lerpDuration);
            _doorMaterial.material.SetColor("_EmissionColor", lerpedColor);
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    private IEnumerator ResetEmissionLerp()
    {
        float t = 0;
        Color lerpedColor;
        while (t < _lerpDuration)
        {
            lerpedColor = Color.Lerp(_encounterColor, _defaultColor, t / _lerpDuration);
            _doorMaterial.material.SetColor("_EmissionColor", lerpedColor);
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
