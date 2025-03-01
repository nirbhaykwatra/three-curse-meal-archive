using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "BridgeEntityCounter", menuName = "Bridge/EntityCounter")]
public class BridgeEntityCount : ScriptableObject
{
    public int _entityCount;
    
    public int EntityCount
    {
        get => _entityCount;
        set => _entityCount = value;
    }

    [Button(ButtonSizes.Large)]
    public void ResetEntityCount()
    {
        _entityCount = 0;
    }
}
