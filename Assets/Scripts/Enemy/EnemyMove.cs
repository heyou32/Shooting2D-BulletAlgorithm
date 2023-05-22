using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [System.Serializable]
    public class Settings
    {
        public float speed = 1.0f;
        [Header("BossEnemy")]
        public bool appear = false;
        public Transform targetPosition;
        [Header("Enemy")]
        public bool useWave = false;
        public float waveRange = 0.5f;
    }
    public Settings setting;
    public Coroutine coroutine;
    bool initialized = false;
    public void Init()
    {
        if (!setting.appear)
            coroutine = StartCoroutine(Move());
        else
            coroutine = StartCoroutine(Appear());
        initialized = true;
    }
    private void OnEnable()
    {
        if (initialized) Init();
    }
    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
    public IEnumerator Move()   // 기본 에너미 움직임
    {
        Vector2 pos = transform.position;
        float x = pos.x;
        while (true)
        {
            if (pos.y < -5.5f)
            {
                gameObject.SetActive(false);
                break;
            }

            pos.y -= Time.deltaTime * setting.speed;
            if (setting.useWave)
                pos.x = (Mathf.Sin(pos.y) * setting.waveRange) + x;

            transform.position = pos;
            yield return null;
        }
    }
    IEnumerator Appear()    // 보스 등장 움직임
    {
        Vector3 pos = transform.position;
        while (pos.y > setting.targetPosition.position.y)
        {
            pos.y -= Time.deltaTime * setting.speed;
            transform.position = pos;
            yield return null;
        }
    }
}
