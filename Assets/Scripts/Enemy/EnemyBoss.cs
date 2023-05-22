 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public override void OnEnable()
    {
        if (onEnabled)
        {
            gameObject.AddComponent<SpawnerBase.RegisterBoss>();
            gameObject.AddComponent<ProgressCounter>();
        }
        onEnabled = true;
    }
}
