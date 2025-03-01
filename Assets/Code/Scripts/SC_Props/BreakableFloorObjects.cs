using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Breakable Floor")]
public class BreakableFloorObjects : ScriptableObject
{
    [PreviewField(100)]
    [LabelWidth(75)]
    public List<GameObject> _floorArt;
    [PreviewField(100)]
    [LabelWidth(75)]
    public List<GameObject> _rocks;

    [HideInInspector]
    public GameObject _rockArt;

    public void SetRockArt()
    {
        GameObject rockArt;
        rockArt = _rocks[Random.Range(0, _rocks.Count)];
        
        _rockArt = rockArt;
    }


}
