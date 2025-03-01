using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LootDrop : ScriptableObject
{
    [Required("Need access to Resources object!", InfoMessageType.Error)]
    public Resources Resources;
    
    [HorizontalGroup("Game Data")]
    [PreviewField(100)]
    [LabelWidth(75)]
    [Required("Loot object does not have art!", InfoMessageType.Warning)]
    public GameObject ArtPrefab;
    
    public virtual void Activate(GameObject target)
    {
        
    }
}