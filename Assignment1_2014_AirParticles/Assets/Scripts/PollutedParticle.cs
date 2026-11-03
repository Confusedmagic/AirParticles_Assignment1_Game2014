using UnityEngine;

public class PollutedParticle : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRadius = 6f;

    private Transform player;
    private bool isBeingDestroyed;
    public bool IsBeingDestroyed => isBeingDestroyed;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null || isBeingDestroyed)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRadius)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    // Both the existing Clean Air Pulse and new bullets use this method.
    public void Defeat(int points)
    {
        if (isBeingDestroyed)
        {
            return;
        }

        isBeingDestroyed = true;

        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(points);
        }

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayRedDestroyed();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isBeingDestroyed || !other.CompareTag("Player"))
        {
            return;
        }

        ParticlePlayer playerScript = other.GetComponent<ParticlePlayer>();
        if (playerScript != null)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayHit();
            }

            playerScript.TakeDamage(1);
            isBeingDestroyed = true;
            Destroy(gameObject);
        }
    }
}
