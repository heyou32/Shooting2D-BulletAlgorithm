using UnityEngine;
public class BulletBase : MonoBehaviour
{
    public BulletType type = BulletType.None;
    public Vector3 velocity;
    public Vector3 saveScale;
    /*
    public static void Aim(BulletBase bullet, GameObject target, float speed, Vector3 position = default)
    {
        if (position != default) bullet.gameObject.transform.position = position;

        bullet.gameObject.SetActive(true);

        var angle = Math2DHelpers.GetAngle(target, bullet.gameObject);
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        bullet.velocity = new Vector3(Mathf.Cos(angle) * speed, Mathf.Sin(angle) * speed);
    }
    */

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    public void ResetBullet()
    {
        var state = gameObject.GetComponentNoAlloc<CollisionState>();
        if (state != null)
            state.ResetCollision();
        transform.localScale = saveScale;
    }
}