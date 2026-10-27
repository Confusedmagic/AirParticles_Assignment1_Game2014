
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAirPulse : MonoBehaviour
{
    public float pulseRadius = 2.5f;
    public float cooldown = 2f;
    public int enemyPoints = 5;
    public SpriteRenderer pulseEffect;

    private float nextAttackTime = 0f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;

        if (pulseEffect != null)
        {
            pulseEffect.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        float size = 1f + Mathf.Sin(Time.time * 4f) * 0.04f;
        transform.localScale = originalScale * size;

        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            UsePulse();
        }
    }

    public void UsePulse()
    {
        if (Time.time < nextAttackTime)
        {
            return;
        }

        nextAttackTime = Time.time + cooldown;

        // pulse sfx
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayPulse();
        }

        PollutedParticle[] enemies =
            FindObjectsByType<PollutedParticle>(
                FindObjectsSortMode.None);

        ScoreManager scoreManager =
            FindFirstObjectByType<ScoreManager>();

        foreach (PollutedParticle enemy in enemies)
        {
            float distance = Vector2.Distance(
                transform.position,
                enemy.transform.position);

            // Plays the enemy destruction sound.
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayRedDestroyed();
            }


            if (distance <= pulseRadius)
            {
                Destroy(enemy.gameObject);

                if (scoreManager != null)
                {
                    scoreManager.AddScore(enemyPoints);
                }
            }
        }


        if (pulseEffect != null)
        {
            StartCoroutine(ShowPulse());
        }
    }

    IEnumerator ShowPulse()
    {
        pulseEffect.transform.position = transform.position;
        pulseEffect.gameObject.SetActive(true);

        float duration = 0.3f;
        float elapsed = 0f;

        Color originalColor = pulseEffect.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsed / duration);

            float size = Mathf.Lerp(
                0.1f, pulseRadius * 2f, progress);

            pulseEffect.transform.localScale =
                new Vector3(size, size, 1f);

            Color color = originalColor;
            color.a = Mathf.Lerp(0.4f, 0f, progress);
            pulseEffect.color = color;

            yield return null;
        }

        pulseEffect.color = originalColor;
        pulseEffect.gameObject.SetActive(false);
    }


}
