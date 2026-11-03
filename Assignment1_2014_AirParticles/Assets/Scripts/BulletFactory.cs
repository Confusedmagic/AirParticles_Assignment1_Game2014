using UnityEngine;

// Factory pattern: other scripts ask this class to create/fire a bullet.
// The factory gets the bullet from the pool instead of instantiating it itself.
public class BulletFactory : MonoBehaviour
{
    public BulletPool bulletPool;

    public void CreateBullet(Vector2 startPosition, Vector2 direction, Transform target)
    {
        if (bulletPool == null)
        {
            Debug.LogWarning("BulletFactory: Assign the BulletPool in the Inspector.", this);
            return;
        }

        PooledBullet bullet = bulletPool.GetBullet();
        if (bullet != null)
        {
            bullet.Launch(startPosition, direction, target);
        }
    }
}
