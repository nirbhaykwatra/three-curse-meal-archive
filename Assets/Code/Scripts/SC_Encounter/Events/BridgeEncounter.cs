using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class BridgeEncounter : Encounter
{
    public BridgeEntityCount _entityCount;
    
    [SerializeField] private int _stage1Threshold;
    [SerializeField] private int _stage2Threshold;
    [SerializeField] private int _collapseThreshold;
    [SerializeField] private BridgeDetectionVolume _detectionVolume;
    [FoldoutGroup("Bridge Encounter Stages", false)]
    [SerializeField] private UnityEvent Stage1;
    [FoldoutGroup("Bridge Encounter Stages", false)]
    [SerializeField] private UnityEvent Stage2;
    [FoldoutGroup("Bridge Encounter Stages", false)]
    [SerializeField] private UnityEvent Collapse;
    private float _timer;
    private bool isStageDone1 = false;
    private bool isStageDone2 = false;

    private void Start()
    {
        _detectionVolume = GetComponentInChildren<BridgeDetectionVolume>();
    }

    public override void StartEncounter()
    {
        OnStarted.Invoke();
    }

    private void Update()
    {
        if (_detectionVolume.PlayerOnBridge)
        {
            if (_entityCount.EntityCount >= _stage1Threshold && !isStageDone1)
            {
                Stage1.Invoke();
                isStageDone1 = true;
            }
            
            if (_entityCount.EntityCount >= _stage2Threshold && !isStageDone2)
            {
                Stage2.Invoke();
                isStageDone2 = true;
            }
        }
    }



    public void DestroyEnemies()
    {
        List<Enemy> enemies = new List<Enemy>();

        enemies.AddRange(GetComponentsInChildren<Enemy>());

        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    public void CollapseBridge()
    {
        Collapse.Invoke();
        OnFinished.Invoke();
    }
}
