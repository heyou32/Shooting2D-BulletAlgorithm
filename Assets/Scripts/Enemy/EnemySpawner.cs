using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : EnemySpawnerBase
{
    // public Queue<GameObject> enemyPool=new Queue<GameObject>();
    public Sprite sprite;
    public RuntimeAnimatorController animator;
    public List<Enemy> enemys = new List<Enemy>();
    public BulletSpawner.Settings bulletSettings = new BulletSpawner.Settings();
    public EnemyMove.Settings moveSettings = new EnemyMove.Settings();
    public float timer = 1;

    int counter = -1;
    public override void Awake()
    {
        Init();
    }
    //private void Awake()
    //{
    //}
    public override void OnSequencerAwake()
    {
        var count = counter;
        ProgressCounter.Add(count);
    }

    public override void OnSequencerUpdate()
    {
        if (timer <= 0)
        {
            counter -= 1;

            if (counter <= 0) enabled = false;
        }
        else
        {
            timer -= 1 * Time.deltaTime;
        }
    }

    private void Init()
    {
        enemys.AddRange(GetComponentsInChildren<Enemy>());
        if (sequencer) sequencer.spawned = enemys.Count;
        counter = enemys.Count;


        for (int i = 0; i < enemys.Count; i++)
        {
            if (bulletSettings.useTheseSettings)
            {
                BulletSpawner bulletSpawner = enemys[i].gameObject.GetComponentNoAlloc<BulletSpawner>();
                if (bulletSpawner == null) bulletSpawner = enemys[i].gameObject.AddComponent<BulletSpawner>();
                enemys[i].bulletSettings = bulletSettings;
                bulletSpawner.mode = bulletSettings.mode;
                bulletSpawner.Set(bulletSettings);
            }

            EnemyMove move = enemys[i].gameObject.GetComponentNoAlloc<EnemyMove>();
            if (move == null) move = enemys[i].gameObject.AddComponent<EnemyMove>();
                move.setting = moveSettings;
            move.Init();

            //enemys[i].Init();
            //enemys[i].moveSettings = moveSettings;
            if (sprite != null)
                enemys[i].gameObject.GetComponentNoAlloc<SpriteRenderer>().sprite = sprite;
            if (animator != null)
                enemys[i].gameObject.GetComponentNoAlloc<Animator>().runtimeAnimatorController = animator;
        }

    }
}
