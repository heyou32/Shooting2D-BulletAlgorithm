 
 
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BulletSpawner.Settings bulletSettings = new BulletSpawner.Settings();
    public bool onEnabled = false;
    // public EnemyMove enemyMove;
    //// public EnemyMove.Settings moveSettings = new EnemyMove.Settings();
    // public Coroutine coroutine;

    //private void Awake()
    //{
    //    Init();
    //}
    public virtual void OnEnable()
    {
        //enemyMove = gameObject.GetComponentNoAlloc<EnemyMove>();
        // coroutine = StartCoroutine(enemyMove.Move());
        if (onEnabled)
        {
            gameObject.AddComponent<SpawnerBase.Register>();
            gameObject.AddComponent<ProgressCounter>();
        }
        onEnabled = true;
        //gameObject.AddComponent<GalagaBehaviour>();

        //var galagaBase = enemy.AddComponent<GalagaBehaviour>();
    }
    // private void OnEnable()
    // {
    // }
    // private void OnDisable()
    // {
    //     if (coroutine != null)
    //         StopCoroutine(coroutine);
    // }


}
