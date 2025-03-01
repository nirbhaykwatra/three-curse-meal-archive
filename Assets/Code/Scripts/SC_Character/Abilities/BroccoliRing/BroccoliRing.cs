using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BroccoliRingVariables", menuName = "Abilities/Brocolli Ring Variables")]
public class BroccoliRing : ScriptableObject
{
    public int _projNumber = 4;
    public float _speed = 10f;
    public float _duration = 10f;
    public List<GameObject> _projectiles;

    [HorizontalGroup("Broccoli Data")]
    [PreviewField(100)]
    [LabelWidth(75)]
    public GameObject _broccoliPrefab;
}
