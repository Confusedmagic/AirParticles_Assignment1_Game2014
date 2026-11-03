using UnityEngine;

// Moves one reusable clean-air projectile toward a polluted particle.
public class PooledBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 4f;
    public int pointsPerHit = 5;

    private BulletPool ownerPool;
    private Rigidbody2D rb;
    private Transform target;
    private Vector2 direction = Vector2.up;
    private float timeRemaining;
    private bool activeBullet;

    public void SetPool(BulletPool pool)
    {
        ownerPool = pool;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 startingPosition, Vector2 startingDirection, Transform newTarget)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        target = newTarget;
        direction = startingDirection.sqrMagnitude > 0.001f
            ? startingDirection.normalized
            : Vector2.up;
        timeRemaining = lifetime;
        activeBullet = true;

        transform.position = startingPosition;
        gameObject.SetActive(true);

        // Keeps physics in sync when reusing an object from the pool.
        if (rb != null)
        {
            rb.position = startingPosition;
        }
    }

    private void FixedUpdate()
    {
        if (!activeBullet)
        {
            return;
        }

        timeRemaining -= Time.fixedDeltaTime;
        if (timeRemaining <= 0f)
        {
            ReturnToPool();
            return;
        }

        // Home in on the nearest red particle selected when fired.
        if (target != null)
        {
            Vector2 toTarget = (Vector2)target.position - (Vector2)transform.position;
            if (toTarget.sqrMagnitude > 0.001f)
            {
                direction = toTarget.normalized;
            }
        }

        Vector2 nextPosition = (Vector2)transform.position +
            direction * speed * Time.fixedDeltaTime;

        if (rb != null)
        {
            rb.MovePosition(nextPosition);
        }
        else
        {
            transform.position = nextPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activeBullet)
        {
            return;
        }

        PollutedParticle enemy = other.GetComponentInParent<PollutedParticle>();
        if (enemy == null || enemy.IsBeingDestroyed)
        {
            return;
        }

        // The enemy owns the sound and score so points are only added once.
        enemy.Defeat(pointsPerHit);
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        if (!activeBullet)
        {
            return;
        }

        activeBullet = false;
        target = null;

        if (ownerPool != null)
        {
            ownerPool.ReturnBullet(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
