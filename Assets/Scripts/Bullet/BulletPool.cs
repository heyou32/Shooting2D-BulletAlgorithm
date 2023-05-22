using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType
{
    None,
    PlayerBlue,
    PlayerGeneric,
    PlayerGreen,
    PlayerPurple,
    PlayerRed,
    PlayerRed2,
    PlayerRed3,

    EnemyBlue,
    EnemyGenereciGreen,
    EnemyGenericRed,
    EnemyGreen,
    EnemyPurple,
    EnemyRed
}

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public Dictionary<BulletType, List<BulletBase>> bulletPool = new Dictionary<BulletType, List<BulletBase>>();

    private void OnEnable()
    {
        Instance = this;
    }

    public BulletBase RentBullet(BulletType type, Vector3 pos, Quaternion rot)
    {
        if (bulletPool.ContainsKey(type))
        {
            if (bulletPool[type].Count > 0)
            {
                BulletBase bullet = bulletPool[type][0];
                bullet.transform.parent = null;
                bullet.transform.position = pos;
                bullet.transform.rotation = rot;
                bullet.gameObject.SetActive(true);
                bulletPool[type].RemoveAt(0);
                return bullet;
            }
        }

        return null;
    }

    public void ReturnBullet(GameObject obj)
    {
        BulletBase bullet = obj.GetComponent<BulletBase>();
        if (bullet == null)
        {
            GameObject.Destroy(obj);
            return;
        }

        bullet.gameObject.SetActive(false);

        if (!bulletPool.ContainsKey(bullet.type))
        {
            bulletPool.Add(bullet.type, new List<BulletBase>());
        }

        bulletPool[bullet.type].Add(bullet);
        bullet.transform.parent = this.transform;
        bullet.ResetBullet();
    }
}
