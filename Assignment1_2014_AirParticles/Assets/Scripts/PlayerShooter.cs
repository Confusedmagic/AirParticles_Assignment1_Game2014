using UnityEngine;
using UnityEngine.InputSystem;

// Attach to the player. Hook Shoot() to a mobile FIRE button's On Click().
public class PlayerShooter : MonoBehaviour
{
    public BulletFactory bulletFactory;
    public Transform firePoint; // Optional: defaults to player's position.
    public float shotCooldown = 0.35f;

    private float nextShotTime;

    private void Update()
    {
        // Press F to test firing from the Unity Editor.
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time < nextShotTime || bulletFactory == null)
        {
            return;
        }

        nextShotTime = Time.time + shotCooldown;
        Vector2 origin = firePoint != null
            ? (Vector2)firePoint.position
            : (Vector2)transform.position;

        // Aim automatically at the closest living red particle.
        PollutedParticle[] enemies = FindObjectsByType<PollutedParticle>(
            FindObjectsSortMode.None);

        PollutedParticle closestEnemy = null;
        float closestDistance = float.PositiveInfinity;

        foreach (PollutedParticle enemy in enemies)
        {
            if (!enemy.isActiveAndEnabled || enemy.IsBeingDestroyed)
            {
                continue;
            }

            float distance = ((Vector2)enemy.transform.position - origin).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        Vector2 direction = Vector2.up;
        Transform target = null;
        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
            direction = ((Vector2)target.position - origin).normalized;
        }

        bulletFactory.CreateBullet(origin, direction, target);
    }
}
