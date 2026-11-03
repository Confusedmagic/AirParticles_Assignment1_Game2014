using System.Collections.Generic;
using UnityEngine;

// Object pool: bullets are recycled instead of being destroyed after every shot.
public class BulletPool : MonoBehaviour
{
    public PooledBullet bulletPrefab;
    public int startingAmount = 15;

    private readonly Queue<PooledBullet> availableBullets = new Queue<PooledBullet>();

    private void Awake()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("BulletPool: Assign the PooledBullet prefab in the Inspector.", this);
            return;
        }

        for (int i = 0; i < startingAmount; i++)
        {
            availableBullets.Enqueue(CreateBullet());
        }
    }

    private PooledBullet CreateBullet()
    {
        PooledBullet bullet = Instantiate(bulletPrefab, transform);
        bullet.SetPool(this);
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    public PooledBullet GetBullet()
    {
        if (bulletPrefab == null)
        {
            return null;
        }

        // Only grow the pool when every existing bullet is in use.
        return availableBullets.Count > 0 ? availableBullets.Dequeue() : CreateBullet();
    }

    public void ReturnBullet(PooledBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(transform);
        availableBullets.Enqueue(bullet);
    }
}
