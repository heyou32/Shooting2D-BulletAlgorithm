using UnityEngine;
public class BulletSpawnerHelper : MonoBehaviour
{
    public static int count;
    void OnEnable() => count += 1;

    void OnDisable() => count -= 1;
}